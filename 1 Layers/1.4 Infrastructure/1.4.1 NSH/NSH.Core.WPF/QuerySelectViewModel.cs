using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace NSH.Core.WPF
{
    public abstract class QuerySelectViewModel<T> : QueryViewModelBase where T : EntityBase
    {
        /// <summary>
        /// 全选命令
        /// </summary>
        public DelegateCommand SelectAllCommand { get; private set; }

        /// <summary>
        /// 实体集合
        /// </summary>
        public IList<SelectEntity<T>> EntitiesList { get; set; }

        public QuerySelectViewModel(IView view, bool isObservable = false)
            : base(view, isObservable)
        {
            SelectAllCommand = new DelegateCommand(this.SelectAllCommandHandler);
        }

        /// <summary>
        /// 查询实体集合，抽象方法
        /// </summary>
        /// <returns></returns>
        protected abstract IList<T> GetEntitiesList();

        /// <summary>
        /// 查询后转化为CollectionView，并通知View
        /// </summary>
        protected override void Query()
        {
            EntitiesList = null;
            OnPropertyChanged("EntitiesList");

            //是否需要转换为ObservableCollection
            EntitiesList = _isObservable ?
                new ObservableCollection<SelectEntity<T>>(SelectEntity<T>.ConvertList(GetEntitiesList()))
                : SelectEntity<T>.ConvertList(GetEntitiesList());

            Count = EntitiesList.Count;
            OnPropertyChanged("EntitiesList");
        }

        #region Select
        protected virtual void SelectAllCommandHandler(object sender, DelegateCommandEventArgs e)
        {
            if (this.EntitiesList != null)
            {
                bool isSelect = Convert.ToBoolean(e.Parameter);
                foreach (var selectEntity in this.EntitiesList)
                {
                    selectEntity.IsSelected = isSelect;
                }
            }
        }

        protected bool TryGetSelectedList(out IList<T> selectedList)
        {
            return TryGetSelectedList(this.EntitiesList, out selectedList);
        }

        protected bool TryGetSelectedList(IList<SelectEntity<T>> entitiesList, out IList<T> selectedList)
        {
            if (entitiesList == null)
            {
                selectedList = null;
                return false;
            }
            selectedList = entitiesList.Where(o => o.IsSelected).Select(o => o.Entity).ToList();
            if (selectedList.Count == 0)
            {
                return false;
            }
            return true;
        }

        protected void SelectList(IList<T> entityList)
        {
            if (this.EntitiesList == null || entityList == null)
            {
                return;
            }
            foreach (var selectEntity in EntitiesList)
            {
                if (entityList.Any(o => o.Id == selectEntity.Entity.Id))
                {
                    selectEntity.IsSelected = true;
                }
            }
        } 
        #endregion
    }
}

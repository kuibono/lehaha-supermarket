using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace NSH.Core.WPF
{
    public abstract class QueryViewModel<T> : QueryViewModelBase where T : EntityBase
    {
        /// <summary>
        /// 实体集合
        /// </summary>
        public IList<T> EntitiesList { get; set; }


        public QueryViewModel(IView view, bool isObservable = false)
            : base(view, isObservable)
        {
        }

        /// <summary>
        /// 查询实体集合，抽象方法
        /// </summary>
        /// <returns></returns>
        protected abstract IList<T> GetEntitiesList();

        /// <summary>
        /// 查询后转化为ObservableCollection，并触发PropertyChanged事件
        /// </summary>
        protected override void Query()
        {
            EntitiesList = null;
            OnPropertyChanged("EntitiesList");

            //是否需要转换为ObservableCollection
            EntitiesList = _isObservable ?
                new ObservableCollection<T>(GetEntitiesList())
                : GetEntitiesList();

            Count = EntitiesList.Count;
            OnPropertyChanged("EntitiesList");
        }
    }
}

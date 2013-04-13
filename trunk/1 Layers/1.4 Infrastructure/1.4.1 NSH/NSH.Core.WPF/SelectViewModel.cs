using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using System.Windows.Data;

namespace NSH.Core.WPF
{
    public abstract class SelectViewModel<T>
        : ViewModelBase where T : EntityBase
    {
        protected virtual T ExcludeEntity { get; private set; }

        /// <summary>
        /// 实体集合视图
        /// </summary>
        public CollectionView EntitiesView { get; protected set; }


        public DelegateCommand SelectCommand { get; private set; }

        public SelectViewModel(ISelectView<T> view, T excludeEntity = null)
            : base(view)
        {
            this.EntitiesView = new ListCollectionView(this.GetEntitiesList().ToList());
            this.OnPropertyChanged("EntitiesView");

            ExcludeEntity = excludeEntity;

            SelectCommand = new DelegateCommand(this.SelectCommandHandler);
        }

        /// <summary>
        /// 查询实体集合，抽象方法
        /// </summary>
        /// <returns></returns>
        protected abstract IList<T> GetEntitiesList();

        protected virtual void SelectCommandHandler(object sender, DelegateCommandEventArgs e)
        {
            var selected = EntitiesView.CurrentItem as T;
            if (selected == null || selected.Id == 0) { return; }

            (View as ISelectView<T>).Selected = selected;

            View.CloseWindow(true);
        }
    }
}

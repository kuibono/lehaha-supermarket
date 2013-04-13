using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace NSH.Core.WPF
{
    public abstract class QueryViewModelBase : ViewModelBase
    {
        protected bool _isObservable;

        #region 属性

        /// <summary>
        /// 查询命令
        /// </summary>
        public DelegateCommand QueryCommand { get; private set; }

        /// <summary>
        /// 新规命令
        /// </summary>
        public DelegateCommand NewCommand { get; private set; }

        /// <summary>
        /// 双击Datagrid行命令
        /// </summary>
        public DelegateCommand DblClickCommand { get; private set; }

        /// <summary>
        /// 删除命令
        /// </summary>
        public DelegateCommand DelCommand { get; private set; }


        private int _count;

        /// <summary>
        /// 数据条数
        /// </summary>
        public virtual int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged("Count");
            }
        }

        #endregion

        #region 构造函数

        public QueryViewModelBase(IView view, bool isObservable = false)
            : base(view)
        {
            QueryCommand = new DelegateCommand(QueryCommandHandler);
            NewCommand = new DelegateCommand(NewCommandHandler);
            DblClickCommand = new DelegateCommand(DblClickCommandHandler);
            DelCommand = new DelegateCommand(DelCommandHandler);
            _isObservable = isObservable;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 查询后转化为ObservableCollection，并触发PropertyChanged事件
        /// </summary>
        protected virtual void Query()
        {

        }

        protected virtual void QueryCommandHandler(object sender, EventArgs e)
        {
            Query();
        }

        protected virtual void NewCommandHandler(object sender, DelegateCommandEventArgs e)
        {

        }

        protected virtual void DblClickCommandHandler(object sender, DelegateCommandEventArgs e)
        {

        }

        protected virtual void DelCommandHandler(object sender, DelegateCommandEventArgs e)
        {

        }

        #endregion
    }
}

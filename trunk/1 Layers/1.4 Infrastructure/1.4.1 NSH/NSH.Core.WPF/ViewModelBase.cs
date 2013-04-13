using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using NSH.Core.Service;

namespace NSH.Core.WPF
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IAsyncLoadingViewModel
    {

        private Func<string, object> _g;
        /// <summary>
        /// ServiceLocatorRegister.ServiceLocator.GetObject简写
        /// </summary>
        protected Func<string, object> G
        {
            get { return _g ?? (_g = ServiceLocatorRegister.ServiceLocator.GetObject); }
        }

        protected IView View;

        protected DelegateCommand cancelCommand;

        protected ViewModelBase()
            : this(null)
        {
        }

        protected ViewModelBase(IView view)
        {
            SetViewAndCancel(view);
        }

        protected virtual void SetViewAndCancel(IView view)
        {
            this.View = view;
            this.cancelCommand = new DelegateCommand(this.CancelCommandHandler);
        }

        public DelegateCommand CancelCommand
        {
            get { return this.cancelCommand; }
        }

        protected virtual void CancelCommandHandler(object sender, EventArgs e)
        {
            this.CloseView();
        }

        protected void CloseView()
        {
            if (this.View != null)
            {
                this.View.CloseWindow();
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IAsyncLoadingViewModel Members

        private Visibility _loadingVisibility = Visibility.Collapsed;

        public Visibility LoadingVisibility
        {
            get
            {
                return _loadingVisibility;
            }
            set
            {
                _loadingVisibility = value;
                OnPropertyChanged("LoadingVisibility");
            }
        }

        #endregion
    }
}

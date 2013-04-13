using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;

namespace NSH.Core.WPF
{
    public class DelegateCommand : ICommand, INotifyPropertyChanged
    {
        public delegate void SimpleEventHandler(object sender, DelegateCommandEventArgs e);

        private SimpleEventHandler handler;
        private bool isEnabled = true;

        public string Tag { get; set; }

        public DelegateCommand(SimpleEventHandler handler)
        {
            this.handler = handler;
        }

        #region ICommand implementation

        public void Execute()
        {
            Execute(null);
        }

        public void Execute(object parameter)
        {
            this.handler(this, new DelegateCommandEventArgs(parameter));
        }

        public bool CanExecute(object parameter)
        {
            return this.IsEnabled;
        }

        public event EventHandler CanExecuteChanged;

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set
            {
                this.isEnabled = value;
                this.OnCanExecuteChanged();
            }
        }

        private void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region AsyncMethod

        public void Async(IAsyncLoadingViewModel asyncLoadingViewModel, Action loader, Action callback = null)
        {
            if (callback == null)
            {
                Async(asyncLoadingViewModel, () => { loader(); return false; });
            }
            else
            {
                Async(asyncLoadingViewModel, () => { loader(); return false; }, r => callback());
            }
        }

        public void Async<T>(IAsyncLoadingViewModel asyncLoadingViewModel, Func<T> loader, Action<T> callback = null)
        {
            if (loader != null)
            {
                IsEnabled = false;
                asyncLoadingViewModel.LoadingVisibility = Visibility.Visible;
                loader.BeginInvoke(ar =>
                {
                    T r = loader.EndInvoke(ar);
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        asyncLoadingViewModel.LoadingVisibility = Visibility.Collapsed;
                        IsEnabled = true;
                        if (callback != null)
                        {
                            callback(r);
                        }
                    }), DispatcherPriority.Render, null);
                }, null);
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}

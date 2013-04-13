using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevComponents.WpfRibbon;

namespace NSH.Core.WPF
{
    public class PopupView : AdvWindow, IView
    {
        #region IView Members

        public IView NextView { get; set; }

        public bool IsConfirmClose
        {
            get;
            private set;
        }

        public void CloseWindow()
        {
            CloseWindow(false);
        }

        public void CloseWindow(bool isConfirmClose)
        {
            IsConfirmClose = isConfirmClose;
            Close();
        }

        public void ShowMessage(string message, string title = "", MessageBoxImage icon = MessageBoxImage.Information)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, icon);
        }

        public bool ShowConfirmMessage(string message, string caption)
        {
            MessageBoxResult result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }

        #endregion
    }
}

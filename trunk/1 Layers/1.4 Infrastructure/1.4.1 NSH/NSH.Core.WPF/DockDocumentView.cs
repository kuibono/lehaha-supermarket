using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using DevComponents.WpfDock;

namespace NSH.Core.WPF
{
    public class DockDocumentView : DockWindow, IView
    {
        #region IView Members

        public IView NextView { get; set; }

        public bool IsConfirmClose
        {
            get;
            private set;
        }

        public new void CloseWindow()
        {
            CloseWindow(false);
        }

        public new void CloseWindow(bool isConfirmClose)
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

        public void Show()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

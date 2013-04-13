using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace NSH.Core.WPF
{
    public interface IView
    {
        IView NextView { get; set; }

        Dispatcher Dispatcher { get; }

        object DataContext { get; set; }

        void Show();

        /// <summary>
        /// 当前窗体是否为确认閉じる
        /// </summary>
        bool IsConfirmClose { get; }

        /// <summary>
        /// 閉じる窗体
        /// </summary>
        void CloseWindow();

        /// <summary>
        /// 閉じる窗体
        /// </summary>
        /// <param name="isConfirmClose">是否为确认閉じる</param>
        void CloseWindow(bool isConfirmClose);

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="icon"></param>
        void ShowMessage(string message, string title = "", MessageBoxImage icon = MessageBoxImage.Information);


        /// <summary>
        /// 显示确认消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        bool ShowConfirmMessage(string message, string caption);

    }
}

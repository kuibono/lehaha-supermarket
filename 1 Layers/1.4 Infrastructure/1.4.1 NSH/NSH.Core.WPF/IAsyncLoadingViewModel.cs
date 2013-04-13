using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NSH.Core.WPF
{
    public interface IAsyncLoadingViewModel
    {
        /// <summary>
        /// 是否显示Loading
        /// </summary>
        Visibility LoadingVisibility { get; set; }
    }
}

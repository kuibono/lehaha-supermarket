using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevComponents.WPF.Controls;
using DevComponents.WpfRibbon;

namespace NSH.Core.WPF
{
    public class StyleProviderManager
    {
        static StyleProviderManager()
        {
            Provider = new StyleProvider();
            Provider.Style = eRibbonVisualStyle.Office2010Blue;
        }
        public static StyleProvider Provider;

        public StyleProvider GetStyle()
        {
            return Provider;
        }
    }
}

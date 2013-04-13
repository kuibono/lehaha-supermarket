using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevComponents.WPF.Controls;
using DevComponents.WpfRibbon;

namespace NSH.Core.WPF
{
    public class StyleProvider : INotifyPropertyChanged
    {
        private eRibbonVisualStyle _style;

        public eRibbonVisualStyle Style
        {
            get { return _style; }
            set
            {
                _style = value;
                OnPropertyChanged("Style");
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
    }
}

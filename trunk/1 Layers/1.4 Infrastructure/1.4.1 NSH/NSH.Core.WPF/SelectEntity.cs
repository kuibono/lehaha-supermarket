using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using NSH.Core.Domain;

namespace NSH.Core.WPF
{
    public class SelectEntity<T> : INotifyPropertyChanged where T : EntityBase
    {
        public SelectEntity(T data)
        {
            IsSelected = false;
            this.Entity = data;
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public T Entity { get; private set; }

        public static IList<SelectEntity<T>> ConvertList(IList<T> list)
        {
            if (list == null)
            {
                return new List<SelectEntity<T>>();
            }
            return list.Select(o => new SelectEntity<T>(o)).ToList();
        }

        public static IList<T> ConvertList(IList<SelectEntity<T>> list)
        {
            if (list == null)
            {
                return new List<T>();
            }
            return list.Select(o => o.Entity).ToList();
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

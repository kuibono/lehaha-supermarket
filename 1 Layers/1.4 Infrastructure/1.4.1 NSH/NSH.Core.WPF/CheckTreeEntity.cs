using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NSH.Core.Domain;

namespace NSH.Core.WPF
{
    /// <summary>
    /// TreeView选择实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CheckTreeEntity<T> : INotifyPropertyChanged where T : EntityBase, ITreeEntity<T>
    {
        /// <summary>
        /// 原始实体
        /// </summary>
        public T Entity { get; private set; }

        /// <summary>
        /// 父级
        /// </summary>
        public CheckTreeEntity<T> Parent { get; private set; }

        /// <summary>
        /// 子级
        /// </summary>
        public IList<CheckTreeEntity<T>> Children { get; private set; }

        private bool? _isChecked;

        /// <summary>
        /// Checkbox选择状态（三态）
        /// </summary>
        public bool? IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked == value)
                {
                    return;
                }
                _isChecked = value;
                UpdateChildIsChecked();
                UpdateParentIsChecked();
                OnPropertyChanged("IsChecked");
            }
        }

        public CheckTreeEntity(T root, IList<T> checkedList)
            : this(root, checkedList, null)
        {
        }

        /// <summary>
        /// 私有构造
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="checkedList"></param>
        /// <param name="parent"></param>
        private CheckTreeEntity(T entity, IList<T> checkedList, CheckTreeEntity<T> parent)
        {
            Entity = entity;
            if (parent != null)
            {
                Parent = parent;
            }
            if (entity.Children != null)
            {
                Children = entity.Children.Select(o => new CheckTreeEntity<T>(o, checkedList, this)).ToList();
            }
            else
            {
                Children = new List<CheckTreeEntity<T>>();
            }
            InitIsChecked(checkedList);
        }

        /// <summary>
        /// 初始化IsChecked
        /// </summary>
        /// <param name="checkedList"></param>
        private void InitIsChecked(IList<T> checkedList)
        {
            if (checkedList.Any(o => o.Id == Entity.Id))
            {
                _isChecked = true;
            }
            else
            {
                _isChecked = GetIsCheckedByChildren();
            }
        }

        /// <summary>
        /// 根据下级确定IsChecked
        /// </summary>
        /// <returns></returns>
        private bool? GetIsCheckedByChildren()
        {
            if (Children != null && Children.Count > 0)
            {
                if (Children.All(o => o.IsChecked.HasValue && o.IsChecked.Value))
                {
                    return true;
                }
                else if (Children.All(o => o.IsChecked.HasValue && !o.IsChecked.Value))
                {
                    return false;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新父级IsChecked
        /// </summary>
        protected virtual void UpdateParentIsChecked()
        {
            if (Parent != null)
            {
                bool? parentNewIsChecked = Parent.GetIsCheckedByChildren();
                if (parentNewIsChecked != Parent._isChecked)
                {
                    Parent._isChecked = parentNewIsChecked;
                    Parent.OnPropertyChanged("IsChecked");
                    Parent.UpdateParentIsChecked();
                }
            }
        }

        /// <summary>
        /// 更新子级IsChecked
        /// </summary>
        protected virtual void UpdateChildIsChecked()
        {
            if (Children != null && _isChecked.HasValue)
            {
                foreach (var child in Children)
                {
                    if (child._isChecked != _isChecked)
                    {
                        child._isChecked = _isChecked;
                        child.OnPropertyChanged("IsChecked");
                        child.UpdateChildIsChecked();
                    }
                }
            }
        }

        public IList<T> GetCheckedList()
        {
            IList<T> checkedList = new List<T>();
            GetCheckedList(checkedList);
            return checkedList;
        }

        private void GetCheckedList(IList<T> checkedList)
        {
            if (_isChecked.HasValue && _isChecked.Value)
            {
                checkedList.Add(Entity);
            }
            if (((_isChecked.HasValue && _isChecked.Value) || !_isChecked.HasValue)
                && Children != null && Children.Count > 0)
            {
                foreach (var child in Children)
                {
                    child.GetCheckedList(checkedList);
                }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using System.ComponentModel;
using System.Windows.Data;
using System.Collections.ObjectModel;

namespace NSH.Core.WPF
{
    public abstract class EditableViewModel<T>
        : ViewModelBase where T : EntityBase
    {

        #region 常量

        private const string currentEntityPropertyName = "CurrentEntity";

        #endregion

        #region 变量

        private IList<BrokenRule> brokenRules;
        private DelegateCommand saveCommand;
        protected int? Id;

        #endregion

        #region 构造函数

        protected EditableViewModel()
            : this(null)
        {
        }

        protected EditableViewModel(IView view)
            : base(view)
        {
            Init();
        }

        protected EditableViewModel(IView view, int id)
            : base(view)
        {
            Id = id;
            Init();
        }

        protected virtual void Init()
        {
            this.brokenRules = new List<BrokenRule>();
            this.CurrentEntity = GetEntity();
            this.saveCommand = new DelegateCommand(SaveCommandHandler);
        }

        #endregion

        #region 抽象方法

        protected abstract T GetEntity();
        protected abstract void SaveCurrentEntity(object sender, DelegateCommandEventArgs e);

        #endregion

        #region 属性

        public IList<BrokenRule> BrokenRules
        {
            get { return this.brokenRules; }
        }

        private T _currentEntity;
        public T CurrentEntity
        {
            get
            {
                return _currentEntity;
            }
            set
            {
                _currentEntity = value;
                OnPropertyChanged("CurrentEntity");
            }
        }

        public DelegateCommand SaveCommand
        {
            get { return this.saveCommand; }
        }


        #endregion

        #region 验证实体

        protected bool ValidateCurrentObject()
        {
            this.brokenRules.Clear();
            ReadOnlyCollection<BrokenRule> currentObjectBrokenRules =
                this.CurrentEntity.GetBrokenRules();
            foreach (BrokenRule rule in currentObjectBrokenRules)
            {
                this.brokenRules.Add(rule);
            }
            return (this.brokenRules.Count == 0);
        }

        #endregion

        #region Command Handlers

        protected void SaveCommandHandler(object sender, DelegateCommandEventArgs e)
        {
            if (this.ValidateCurrentObject())
            {
                this.SaveCurrentEntity(sender, e);
                this.OnPropertyChanged("CurrentEntity");
            }
            else
            {
                View.ShowMessage(this.BrokenRules.FirstOrDefault().Description);
            }
        }

        #endregion

    }
}

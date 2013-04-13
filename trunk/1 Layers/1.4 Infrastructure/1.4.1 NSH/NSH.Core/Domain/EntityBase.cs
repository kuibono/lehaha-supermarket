using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using System.Collections.ObjectModel;

namespace NSH.Core.Domain
{
    public abstract class EntityBase : IEntity
    {
        private int _id;
        private List<BrokenRule> brokenRules;



        protected EntityBase()
        {
            this.brokenRules = new List<BrokenRule>();
        }

        public virtual int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }


        #region Validation and Broken Rules

        protected abstract void Validate();

        protected List<BrokenRule> BrokenRules
        {
            get { return this.brokenRules; }
        }

        public ReadOnlyCollection<BrokenRule> GetBrokenRules()
        {
            this.brokenRules.Clear();
            this.Validate();
            return this.brokenRules.AsReadOnly();
        }

        protected void AddBrokenRule(string propertyName, string message)
        {
            this.brokenRules.Add(new BrokenRule(propertyName, message));
        }

        #endregion

        #region Equality Tests


        public override bool Equals(object entity)
        {
            return entity != null
                && entity is EntityBase
                && this == (EntityBase)entity;
        }


        public static bool operator ==(EntityBase base1,
            EntityBase base2)
        {
            if ((object)base1 == null && (object)base2 == null)
            {
                return true;
            }

            if ((object)base1 == null || (object)base2 == null)
            {
                return false;
            }

            if (base1.Id != base2.Id)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(EntityBase base1,
            EntityBase base2)
        {
            return (!(base1 == base2));
        }


        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion
    }
}

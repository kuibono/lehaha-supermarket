using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Category
{
    ///<summary>
    ///表bs_pa_class的实体类
    ///</summary>
    public class BsPaClass : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string ClassName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public BsPaClass Clone()
        {
            return (BsPaClass)this.MemberwiseClone();
        }
    }

}


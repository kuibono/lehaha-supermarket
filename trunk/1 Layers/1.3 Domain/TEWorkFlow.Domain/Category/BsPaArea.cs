using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Category
{
    ///<summary>
    ///表bs_pa_area的实体类
    ///</summary>
    public class BsPaArea : EntityGUIDBase, IAggregateRootGUID
    {
        
        /// <summary>
        /// 
        /// </summary>
        public virtual string AreaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Functionary { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public BsPaArea Clone()
        {
            return (BsPaArea)this.MemberwiseClone();
        }
    }

}
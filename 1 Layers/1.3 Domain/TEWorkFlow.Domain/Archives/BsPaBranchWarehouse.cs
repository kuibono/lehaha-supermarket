/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/9 20:42:47
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表bs_pa_branch_warehouse的实体类
    ///</summary>
    public class BsPaBranchWarehouse : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string bCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal WhArea { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public BsPaBranchWarehouse Clone()
        {
            return (BsPaBranchWarehouse)this.MemberwiseClone();
        }
    }

}



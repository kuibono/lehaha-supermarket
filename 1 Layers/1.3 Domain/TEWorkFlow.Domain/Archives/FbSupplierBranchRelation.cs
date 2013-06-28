/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/6/28 21:48:41
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表fb_supplier_branch_relation的实体类
    ///</summary>
    public class FbSupplierBranchRelation : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string SupCode { get; set; }

        public virtual string SupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string bCode { get; set; }

        public virtual string bName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Available { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbSupplierBranchRelation Clone()
        {
            return (FbSupplierBranchRelation)this.MemberwiseClone();
        }
    }

}



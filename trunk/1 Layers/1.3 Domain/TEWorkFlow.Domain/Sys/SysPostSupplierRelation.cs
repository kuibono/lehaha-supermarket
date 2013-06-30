/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/6/30 21:43:23
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Sys
{
    ///<summary>
    ///表sys_Post_Supplier_Relation的实体类
    ///</summary>
    public class SysPostSupplierRelation : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string PostId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Readed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime ReadTime { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public SysPostSupplierRelation Clone()
        {
            return (SysPostSupplierRelation)this.MemberwiseClone();
        }
    }

}



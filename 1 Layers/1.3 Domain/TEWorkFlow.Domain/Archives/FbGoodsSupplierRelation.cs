﻿/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/6/17 20:27:31
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表fb_goods_supplier_relation的实体类
    ///</summary>
    public class FbGoodsSupplierRelation : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string bCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IfEnable { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbGoodsSupplierRelation Clone()
        {
            return (FbGoodsSupplierRelation)this.MemberwiseClone();
        }
    }

}



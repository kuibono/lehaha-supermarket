/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/3 20:21:45
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Business
{
    ///<summary>
    ///表pc_purchase_detail的实体类
    ///</summary>
    public class PcPurchaseDetailHistory : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string ManageId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsBarCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Specification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PackUnitCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OfferMin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? StockQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OrderQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PackQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PackCoef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PurchaseQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PutinQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SalePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PurchasePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? NontaxPurchasePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PurchaseMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? NontaxPurchaseMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ProduceDate { get; set; }

        public virtual bool IsFree { get; set; }

        public virtual DateTime AddTime { get; set; }


        public virtual string GoodsName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public PcPurchaseDetail Clone()
        {
            return (PcPurchaseDetail)this.MemberwiseClone();
        }
    }

}



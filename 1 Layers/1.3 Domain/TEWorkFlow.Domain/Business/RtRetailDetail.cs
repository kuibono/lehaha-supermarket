/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/7/4 20:23:02
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Business
{
    ///<summary>
    ///表rt_retail_detail的实体类
    ///</summary>
    public class RtRetailDetail : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SaFlowNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RtNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string sNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime RtDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RtType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string bCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Posid { get; set; }

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
        public virtual decimal SalePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal RealPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SaleQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SaleMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal DiscountMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal PurchaseQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal pSalePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal pSaleMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal pDiscountMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Cashier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Salesman { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UnderCounterCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal PoolRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime ShiftDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfPromotion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OwnerType { get; set; }


        public virtual string GoodsName { get; set; }
        protected override void Validate()
        {
        }
        ///实体复制
        public RtRetailDetail Clone()
        {
            return (RtRetailDetail)this.MemberwiseClone();
        }
    }

}



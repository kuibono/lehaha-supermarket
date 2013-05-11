/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/11 22:37:25
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Business
{
    ///<summary>
    ///表pc_return_detail的实体类
    ///</summary>
    public class PcReturnDetail : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string RtNumber { get; set; }

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
        public virtual decimal? PackQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PackCoef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? ReturnQty { get; set; }

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
        public virtual decimal? ReturnMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? NontaxReturnMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SaleMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? NontaxSaleMoney { get; set; }








        public virtual string GoodsName { get; set; }


        protected override void Validate()
        {
        }
        ///实体复制
        public PcReturnDetail Clone()
        {
            return (PcReturnDetail)this.MemberwiseClone();
        }
    }

}



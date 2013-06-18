/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/6/18 21:04:36
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表fb_goods_archives_bar的实体类
    ///</summary>
    public class FbGoodsArchivesBar : EntityGUIDBase, IAggregateRootGUID
    {
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
        public virtual string GoodsBarName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PackUnitCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal PackCoef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string QtyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PriceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SalePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal VipPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal TradePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal PushRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfMainBar { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfExamine { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbGoodsArchivesBar Clone()
        {
            return (FbGoodsArchivesBar)this.MemberwiseClone();
        }
    }

}



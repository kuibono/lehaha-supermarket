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
        public virtual string GoodsBarName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PackUnitCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private decimal _PackCoef = 1;
        public virtual decimal PackCoef { get { return _PackCoef; } set { _PackCoef = value; } }

        /// <summary>
        /// 
        /// </summary>
        private string _QtyType = "1";
        public virtual string QtyType
        {
            get
            {
                return
                    _QtyType;
            }
            set { _QtyType = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PriceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private decimal _SalePrice = 0;
        public virtual decimal SalePrice { get { return _SalePrice; } set { _SalePrice = value; } }

        /// <summary>
        /// 
        /// </summary>
        private decimal _VipPrice = 0;
        public virtual decimal VipPrice { get { return _VipPrice; } set { _VipPrice = value; } }

        /// <summary>
        /// 
        /// </summary>
        private decimal _TradePrice = 0;
        public virtual decimal TradePrice { get { return _TradePrice; } set { _TradePrice = value; } }

        /// <summary>
        /// 
        /// </summary>
        private decimal _PushRate = 0;
        public virtual decimal PushRate { get { return _PushRate; } set { _PushRate = value; } }

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



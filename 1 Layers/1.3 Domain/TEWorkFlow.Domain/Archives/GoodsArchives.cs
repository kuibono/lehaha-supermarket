using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表fb_goods_archives的实体类
    ///</summary>
    public class GoodsArchives : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsBarCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsSubCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GbCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GlCode { get; set; }

        private string _GoodsType = "1";
        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsType
        {
            get
            {
                if (string.IsNullOrEmpty(_GoodsType))
                {
                    return "1";
                }
                return _GoodsType;
            }
            set
            {
                _GoodsType = value;
            }
        }


        private string _CheckMode = "1";
        /// <summary>
        /// 
        /// </summary>
        public virtual string CheckMode
        {
            get
            {
                if (string.IsNullOrEmpty(_CheckMode))
                {
                    return "1";
                }
                return _CheckMode;
            }
            set
            {
                _CheckMode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupCode { get; set; }



        private string _OpCode = "1";
        /// <summary>
        /// 
        /// </summary>
        public virtual string OpCode
        {
            get
            {
                if (string.IsNullOrEmpty(_OpCode))
                {
                    return "1";
                }
                return _OpCode;
            }
            set
            {
                _OpCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PoolRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsSubName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PyCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ProducingArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ArticleNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Specification { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ShelfLife { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PackUnitCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OfferMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PackCoef { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OfferMin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? InputTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OutputTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? StockUpperLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? StockLowerLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UnderFloorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UnderCounterCode { get; set; }


        private string _CheckUnitCode = "0";
        /// <summary>
        /// 
        /// </summary>
        public virtual string CheckUnitCode
        {
            get
            {
                if (string.IsNullOrEmpty(_CheckMode))
                {
                    return "0";
                }
                return _CheckUnitCode;
            }
            set
            {
                _CheckUnitCode = value;
            }
        }

        private decimal? _PurchasePrice = 0;

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PurchasePrice
        {
            get
            {
                if (_PurchasePrice.HasValue == false)
                {
                    return 0;
                }
                return _PurchasePrice;
            }
            set
            {
                _PurchasePrice = value;
            }
        }

        private decimal? _NontaxPurchasePrice = 0;

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? NontaxPurchasePrice
        {
            get
            {
                if (_NontaxPurchasePrice.HasValue == false)
                {
                    return 0;
                }
                return _NontaxPurchasePrice;
            }
            set {
                _NontaxPurchasePrice = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? AvgCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? NontaxAvgCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? GrossRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? SalePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? VipPrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? TradePrice { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PushRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Operator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Assessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfExamine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ExamineDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? OperatorDate { get; set; }

        public virtual string Remark { get; set; }

        /// <summary>
        /// 建议价格
        /// </summary>
        public virtual decimal? ProposePrice { get; set; }

        public virtual string PriceHistory { get; set; }

        public virtual string BackupCode { get; set; }

        public virtual string IfNew { get; set; }




        public virtual string GbName { get; set; }

        public virtual string GmName { get; set; }

        public virtual string GsName { get; set; }

        public virtual string GlName { get; set; }

        public virtual string SupName { get; set; }

        public virtual string SupTel { get; set; }


        protected override void Validate()
        {
        }
        ///实体复制
        public GoodsArchives Clone()
        {
            return (GoodsArchives)this.MemberwiseClone();
        }
    }

}

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
        /// 
        private string _GbCode = "";
        public virtual string GbCode
        {
            get { return _GbCode; }
            set
            {
                if (value == null)
                    return; 
                _GbCode = value;
            }
        }


        private string _GmCode = "";

        public virtual string GmCode
        {
            get { return _GmCode; }
            set
            {
                if (value == null)
                    return; 
                _GmCode = value;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private string _GsCode = "";
        public virtual string GsCode
        {
            get { return _GsCode; }
            set
            {
                if (value == null)
                    return;
                _GsCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _GlCode = "";
        public virtual string GlCode
        {
            get { return _GlCode; }
            set
            {
                if (value == null)
                    return;
                _GlCode = value;
            }
        }

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
                if (value == null)
                    return; 
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
                if (value == null)
                    return; 
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
                if (value == null)
                    return; 
                _OpCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal PoolRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string _GoodsSubName = "";
        public virtual string GoodsSubName
        {
            get
            {
                if (_GoodsSubName != null && _GoodsSubName.Length > 0)
                    return _GoodsSubName;
                if (GoodsName == null)
                    return _GoodsSubName;
                return GoodsName.Length > 10 ? GoodsName.Substring(10) : GoodsName;

            }
            set
            {
                if (value == null)
                    return; 
                _GoodsSubName = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PyCode { get; set; }


        private string _GoodsState = "1";
        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsState
        {
            get
            {
                if (string.IsNullOrEmpty(_GoodsState)) { return "1"; }
                return _GoodsState;
            }
            set
            {
                if (value == null)
                    return; 
                _GoodsState = value;
            }
        }

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

        private string _PackUnitCode = "01";
        /// <summary>
        /// 
        /// </summary>
        public virtual string PackUnitCode
        {
            get
            {
                if (string.IsNullOrEmpty(_PackUnitCode))
                    return "01";
                return _PackUnitCode;
            }
            set
            {
                if (value == null)
                    return; 
                _PackUnitCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string _OfferMode = "1";
        public virtual string OfferMode
        {
            get { return _OfferMode; }
            set
            {
                if (value == null)
                    return; 
                _OfferMode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private decimal _PackCoef = 1;
        public virtual decimal PackCoef
        {
            get { return _PackCoef; }
            set { _PackCoef = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        private decimal _OfferMin = 1;
        public virtual decimal OfferMin
        {
            get { return _OfferMin; }
            set { _OfferMin = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal InputTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal OutputTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal StockUpperLimit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal StockLowerLimit { get; set; }

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
                if (value == null)
                    return; 
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
                if (value == null)
                    return; 
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
            set
            {
                if (value == null)
                    return; 
                _NontaxPurchasePrice = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal AvgCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal NontaxAvgCost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal GrossRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private decimal _SalePrice = 0;
        public virtual decimal SalePrice
        {
            get { return _SalePrice > 0 ? _SalePrice : ProposePrice; }
            set
            {
                if (value == null)
                    return; 
                _SalePrice = value;
            }
        }

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
        private decimal _ProposePrice = 0;
        public virtual decimal ProposePrice { get { return _ProposePrice; } set { _ProposePrice = value; } }

        public virtual string PriceHistory { get; set; }

        public virtual string BackupCode { get; set; }

        public virtual string IfNew { get; set; }




        public virtual string GbName { get; set; }

        public virtual string GmName { get; set; }

        public virtual string GsName { get; set; }

        public virtual string GlName { get; set; }

        public virtual string SupName { get; set; }

        public virtual string SupTel { get; set; }


        private string _QtyType = "1";
        public virtual string QtyType
        {
            get
            {
                if ( _QtyType!="2")
                {
                    return "1";
                }
                return _QtyType;
            }
            set { _QtyType = value; }
        }


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

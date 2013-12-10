using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表fb_goods_archives_supplier的实体类
    ///</summary>
    public class FbGoodsArchivesSupplier : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual string GoodsCode { get; set; }

        /// <summary>
        /// 供货商编码
        /// </summary>
        public virtual string SupCode { get; set; }

        /// <summary>
        /// 供货商名称
        /// </summary>
        public virtual string SupName { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public virtual string PyCode { get; set; }

        /// <summary>
        /// 经营方式
        /// </summary>
        private string _OpCode = "1";
        public virtual string OpCode
        {
            get { return _OpCode; }
            set
            {
                if (value == null)
                    return;
                _OpCode = value;
            }
        }

        /// <summary>
        /// 扣率
        /// </summary>
        private decimal _PoolRate = 0;
        public virtual decimal PoolRate { get { return _PoolRate; } set { _PoolRate = value; } }

        /// <summary>
        /// 供货方式
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
        /// 最小订量
        /// </summary>
        private decimal _OfferMin = 1;
        public virtual decimal OfferMin { get { return _OfferMin; } set { _OfferMin = value; } }

        /// <summary>
        /// 进项税率
        /// </summary>
        private decimal _InputTax = 0;
        public virtual decimal InputTax { get { return _InputTax; } set { _InputTax = value; } }

        /// <summary>
        /// 含税进价
        /// </summary>
        public virtual decimal PurchasePrice { get; set; }

        /// <summary>
        /// 不含税进价
        /// </summary>
        private decimal _NontaxPurchasePrice = 0;
        public virtual decimal NontaxPurchasePrice
        {
            get
            {

                if (_NontaxPurchasePrice == 0)
                {
                    return PurchasePrice;
                }
                return _NontaxPurchasePrice;
            }
            set { _NontaxPurchasePrice = value; }
        }

        /// <summary>
        /// 是否主供货商
        /// </summary>
        public virtual string IfMainSupplier { get; set; }

        /// <summary>
        /// 是否审核
        /// </summary>
        public virtual string IfExamine { get; set; }


        protected override void Validate()
        {
        }
        ///实体复制
        public FbGoodsArchivesSupplier Clone()
        {
            return (FbGoodsArchivesSupplier)this.MemberwiseClone();
        }
    }

}

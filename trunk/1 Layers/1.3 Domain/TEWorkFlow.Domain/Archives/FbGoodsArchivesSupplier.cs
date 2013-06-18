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
        public virtual string OpCode { get; set; }

        /// <summary>
        /// 扣率
        /// </summary>
        public virtual decimal PoolRate { get; set; }

        /// <summary>
        /// 供货方式
        /// </summary>
        public virtual string OfferMode { get; set; }

        /// <summary>
        /// 最小订量
        /// </summary>
        public virtual decimal OfferMin { get; set; }

        /// <summary>
        /// 进项税率
        /// </summary>
        public virtual decimal InputTax { get; set; }

        /// <summary>
        /// 含税进价
        /// </summary>
        public virtual decimal PurchasePrice { get; set; }

        /// <summary>
        /// 不含税进价
        /// </summary>
        public virtual decimal NontaxPurchasePrice { get; set; }

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

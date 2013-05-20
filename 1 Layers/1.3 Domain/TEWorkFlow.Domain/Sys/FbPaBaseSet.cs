/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/20 21:55:02
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Sys
{
    ///<summary>
    ///表fb_pa_base_set的实体类
    ///</summary>
    public class FbPaBaseSet : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 大类位数
        /// </summary>
        public virtual decimal GoodsGbLen { get; set; }

        /// <summary>
        /// 中类位数
        /// </summary>
        public virtual decimal GoodsGmLen { get; set; }

        /// <summary>
        /// 小类位数
        /// </summary>
        public virtual decimal GoodsGsLen { get; set; }

        /// <summary>
        /// 细类位数
        /// </summary>
        public virtual decimal GoodsGlLen { get; set; }

        /// <summary>
        /// 商品分类级数
        /// </summary>
        public virtual decimal GoodsClassLevel { get; set; }

        /// <summary>
        /// 商品流水码位数
        /// </summary>
        public virtual decimal GoodsLen { get; set; }

        /// <summary>
        /// 前缀类别
        /// </summary>
        public virtual string GoodsPrefix { get; set; }

        /// <summary>
        /// 供货商流水码位数
        /// </summary>
        public virtual decimal SupLen { get; set; }

        /// <summary>
        /// 前缀方式
        /// </summary>
        public virtual string SupPrefixType { get; set; }

        /// <summary>
        /// 前缀内容
        /// </summary>
        public virtual string SupPrefixContent { get; set; }

        /// <summary>
        /// 进价小数位数
        /// </summary>
        public virtual decimal PurchasePreciseLen { get; set; }

        /// <summary>
        /// 销售价小数位数
        /// </summary>
        public virtual decimal SalePreciseLen { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public virtual string Operator { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual System.DateTime OperatorDate { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbPaBaseSet Clone()
        {
            return (FbPaBaseSet)this.MemberwiseClone();
        }
    }

}



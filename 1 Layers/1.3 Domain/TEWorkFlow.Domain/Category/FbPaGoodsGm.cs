/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/4/30 21:20:57
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Category
{
    ///<summary>
    ///表fb_pa_goods_gm的实体类
    ///</summary>
    public class FbPaGoodsGm : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string GbCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string GmName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbPaGoodsGm Clone()
        {
            return (FbPaGoodsGm)this.MemberwiseClone();
        }
    }

}



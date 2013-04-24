/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/4/24 21:10:27
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Category
{
    ///<summary>
    ///表fb_pa_goods_gb的实体类
    ///</summary>
    public class FbPaGoodsGb : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string GbName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbPaGoodsGb Clone()
        {
            return (FbPaGoodsGb)this.MemberwiseClone();
        }
    }

}



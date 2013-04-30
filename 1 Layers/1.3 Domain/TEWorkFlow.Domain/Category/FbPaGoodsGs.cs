/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/4/30 21:21:28
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Category
{
    ///<summary>
    ///表fb_pa_goods_gs的实体类
    ///</summary>
    public class FbPaGoodsGs : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string GmCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string GsName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbPaGoodsGs Clone()
        {
            return (FbPaGoodsGs)this.MemberwiseClone();
        }
    }

}



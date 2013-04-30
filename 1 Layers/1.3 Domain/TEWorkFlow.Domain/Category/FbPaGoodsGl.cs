/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/4/30 21:20:17
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Category
{
    ///<summary>
    ///表fb_pa_goods_gl的实体类
    ///</summary>
    public class FbPaGoodsGl : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string GsCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual string GlName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbPaGoodsGl Clone()
        {
            return (FbPaGoodsGl)this.MemberwiseClone();
        }
    }

}



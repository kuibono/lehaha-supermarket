/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/6/15 13:45:48
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Sys
{
    ///<summary>
    ///表BranchPost的实体类
    ///</summary>
    public class BranchPost : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime PostTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PostUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PostContent { get; set; }

        public virtual bool Readed { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public SysPost Clone()
        {
            return (SysPost)this.MemberwiseClone();
        }
    }

}



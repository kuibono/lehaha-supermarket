/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/5 20:19:56
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Sys
{
    ///<summary>
    ///表sys_pa_department的实体类
    ///</summary>
    public class SysPaDepartment : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string dName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Higher { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public SysPaDepartment Clone()
        {
            return (SysPaDepartment)this.MemberwiseClone();
        }
    }

}



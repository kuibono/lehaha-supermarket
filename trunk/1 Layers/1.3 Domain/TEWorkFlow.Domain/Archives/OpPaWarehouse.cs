/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/9 20:42:21
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表op_pa_warehouse的实体类
    ///</summary>
    public class OpPaWarehouse : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal WhArea { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfMainWh { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public OpPaWarehouse Clone()
        {
            return (OpPaWarehouse)this.MemberwiseClone();
        }
    }

}



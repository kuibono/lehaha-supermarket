/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/11 22:36:18
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Business
{
    ///<summary>
    ///表pc_return_manage的实体类
    ///</summary>
    public class PcReturnManage : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string PcNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string EnCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PcForm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string dCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string bCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ReturnDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfPutout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? PutoutDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CreateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Operator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Assessor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfExamine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ExamineDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? OperatorDate { get; set; }





        public virtual string dName { get; set; }
        public virtual string bName { get; set; }
        protected override void Validate()
        {
        }
        ///实体复制
        public PcReturnManage Clone()
        {
            return (PcReturnManage)this.MemberwiseClone();
        }
    }

}



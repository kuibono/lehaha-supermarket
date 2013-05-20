using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain.Business
{
    public class PcSupplementManage : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual string PcCode { get; set; }

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
        public virtual string PcType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PcMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? ExpectArriveDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfCheck { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? CheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string IfPutin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime? PutinDate { get; set; }

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

        /// <summary>
        /// 补货金额
        /// </summary>
        public virtual decimal? MoneyAmount { get; set; }


        public virtual string dName { get; set; }
        public virtual string bName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public PcPurchaseManage Clone()
        {
            return (PcPurchaseManage)this.MemberwiseClone();
        }
    }
}

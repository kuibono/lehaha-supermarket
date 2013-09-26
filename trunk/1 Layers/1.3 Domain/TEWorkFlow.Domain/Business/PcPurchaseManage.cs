/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/5/3 20:21:11
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Business
{
    ///<summary>
    ///表pc_purchase_manage的实体类
    ///</summary>
    public class PcPurchaseManage : EntityGUIDBase, IAggregateRootGUID
    {

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

        private string _PcType = "普通订货";
        /// <summary>
        /// 
        /// </summary>
        public virtual string PcType { get{ return _PcType;} set{_PcType=value;} }

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





        public virtual string dName { get; set; }

        public virtual string bName { get; set; }

        public virtual string operatorName { get; set; }

        public virtual int detailCount { get; set; }

        public virtual int count { get; set; }

        public virtual decimal? amount { get; set; }

        private bool _IfMoved = false;
        public virtual bool IfMoved { get { return _IfMoved; } set { _IfMoved = value; } }

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



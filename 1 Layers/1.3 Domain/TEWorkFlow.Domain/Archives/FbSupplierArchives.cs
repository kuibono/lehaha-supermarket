/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/4/20 12:16:40
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表fb_supplier_archives的实体类
    ///</summary>
    public class FbSupplierArchives : EntityGUIDBase, IAggregateRootGUID
    {
       
        /// <summary>
        /// 
        /// </summary>
        public virtual string SupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PyCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupTypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Functionary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FunctionaryPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Linkman { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ContactPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ContactAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OfficePhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaxPhone { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string eMail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Postalcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OpenAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BankAccount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TaxNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OpCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string StockVoucher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? InputTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BalanceMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PayMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string BalancePeriod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? BalanceDay { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string OfferMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? DeliveryDays { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PoolRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? FloorsMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? ExcessRate { get; set; }

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





        public virtual string SupTypeName { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public FbSupplierArchives Clone()
        {
            return (FbSupplierArchives)this.MemberwiseClone();
        }
    }

}



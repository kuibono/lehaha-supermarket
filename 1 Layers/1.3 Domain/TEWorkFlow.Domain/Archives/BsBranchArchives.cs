
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Archives
{
    ///<summary>
    ///表bs_branch_archives的实体类
    ///</summary>
    public class BsBranchArchives : EntityGUIDBase, IAggregateRootGUID
    {

        /// <summary>
        /// 
        /// </summary>
        public virtual string bName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PyCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string bType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string bState { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string AreaCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ClassCode { get; set; }

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

        private string _OpCode = "0";
        /// <summary>
        /// 
        /// </summary>
        public virtual string OpCode {
            get
            {
                if(string.IsNullOrEmpty(_OpCode))
                {
                    return "0";
                }
                return _OpCode;
            }
            set{
                _OpCode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string StockVoucher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal InputTax { get; set; }

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
        public virtual decimal? DeliveryDays { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupplyPriceType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? GrossRate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhCode { get; set; }

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

        public virtual string Remark { get; set; }

        public virtual string IfSend { get; set; }

        public virtual string IfSyncSale { get; set; }


        public virtual string ClassName { get; set; }
        public virtual string AreaName { get; set; }

        private string _InputBCode;
        public virtual string InputBCode
        {
            get
            {
                if (string.IsNullOrEmpty(_InputBCode))
                {
                    return Id;
                }
                else
                {
                    return _InputBCode;
                }
            }
            set
            {
                _InputBCode = value;
            }
        }

        public virtual string SyncPassword { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public BsBranchArchives Clone()
        {
            return (BsBranchArchives)this.MemberwiseClone();
        }
    }

}



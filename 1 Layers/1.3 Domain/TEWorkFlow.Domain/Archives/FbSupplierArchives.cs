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


        private string _OpCode = "1";
        /// <summary>
        /// 
        /// </summary>
        public virtual string OpCode
        {
            get
            {
                if(string.IsNullOrEmpty(_OpCode))
                {
                    return "1";
                }
                return _OpCode;
            }
            set
            {
                _OpCode = value;
            }
        }


        private string _StockVoucher = "增值税发票";
        /// <summary>
        /// 
        /// </summary>
        public virtual string StockVoucher
        {
            get
            {

                if (string.IsNullOrEmpty(_StockVoucher))
                {
                    return "增值税发票";
                }
                return _StockVoucher;
            }
            set
            {
                _StockVoucher = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? InputTax { get; set; }


        private string _BalanceMode = "货到付款";
        /// <summary>
        /// 
        /// </summary>
        public virtual string BalanceMode
        {
            get
            {
                if (string.IsNullOrEmpty(_BalanceMode))
                {
                    return "货到付款";
                }
                return _BalanceMode;
            }
            set
            {
                _BalanceMode = value;
            }
        }


        private string _PayMode = "现金";
        /// <summary>
        /// 
        /// </summary>
        public virtual string PayMode
        {
            get
            {
                if (string.IsNullOrEmpty(_PayMode))
                {
                    return "现金";
                }
                return _PayMode;
            }
            set
            {
                _PayMode = value;
            }
        }


        private string _BalancePeriod = "月结算";
        /// <summary>
        /// 
        /// </summary>
        public virtual string BalancePeriod
        {
            get
            {
                if (string.IsNullOrEmpty(_BalancePeriod))
                {
                    return "月结算";
                }
                return _BalancePeriod;
            }
            set
            {
                _BalancePeriod = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? BalanceDay { get; set; }

        private string _OfferMode = "1";
        /// <summary>
        /// 
        /// </summary>
        public virtual string OfferMode
        {
            get
            {
                if (string.IsNullOrEmpty(_OfferMode))
                {
                    return "1";
                }
                return _OfferMode;
            }
            set
            {
                _OfferMode = value;
            }
        }

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


        public virtual string LoginName { get; set; }
        public virtual string LoginPass { get; set; }
        public virtual string Remark { get; set; }






        public virtual int GoodsCount { get; set; }

        public virtual string TreeDisplayText
        {
            get
            {
                return string.Format("{0}({1})", SupName, GoodsCount);
            }
        }


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



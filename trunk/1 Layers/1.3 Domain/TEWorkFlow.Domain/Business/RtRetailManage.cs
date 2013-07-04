/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/7/4 20:22:16
*生成者：kuibono
*/
using System;
using NSH.Core.Domain;
namespace TEWorkFlow.Domain.Business
{
    ///<summary>
    ///表rt_retail_manage的实体类
    ///</summary>
    public class RtRetailManage : EntityGUIDBase, IAggregateRootGUID
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SaFlowNumber { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime RtDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RtType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string WhCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string bCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Posid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SaleQty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal SaleMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal DiscountMoney { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CardNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Cashier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CashierHigher { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Salesman { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string UnderCounterCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual System.DateTime ShiftDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ReturnNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Comment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SysGuid { get; set; }


        public virtual int detailCount { get; set; }

        public virtual int count { get; set; }

        public virtual decimal? amount { get; set; }

        protected override void Validate()
        {
        }
        ///实体复制
        public RtRetailManage Clone()
        {
            return (RtRetailManage)this.MemberwiseClone();
        }
    }

}



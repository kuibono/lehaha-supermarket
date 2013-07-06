using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;

namespace TEWorkFlow.Domain
{
    public class RetailPurchaseCompare : EntityGUIDBase, IAggregateRootGUID
    {
        public virtual string bCode { get; set; }

        public virtual string bName { get; set; }

        public virtual string GoodsCode { get; set; }

        public virtual string GoodsName { get; set; }

        public virtual string BarCode { get; set; }

        public virtual decimal RetailCount { get; set; }

        public virtual decimal RetailMoney { get; set; }

        public virtual decimal PurchaseCount { get; set; }

        public virtual decimal PurchaseMoney { get; set; }

        protected override void Validate()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public class PurchaseSupplierResult
    {
        public virtual string SupName { get; set; }

        public virtual int goodsCount { get; set; }

        public virtual decimal? amount{get;set;}
    }
}

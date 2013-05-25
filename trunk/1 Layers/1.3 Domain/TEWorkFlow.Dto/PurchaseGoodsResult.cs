using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TEWorkFlow.Dto
{
    public class PurchaseGoodsResult
    {


        public virtual string GoodsCode { get; set; }


        public virtual string GoodsName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string GoodsBarCode { get; set; }

        public virtual string SupName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? OrderQty { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public virtual decimal? PurchasePrice { get; set; }


        public virtual decimal? Amount { get; set; }

        public virtual int count { get; set; }
    }
}

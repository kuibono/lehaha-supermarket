using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NSH.Core.DataPage;

namespace TEWorkFlow.Web.Client.Models
{
    public class DataGridPage : IDataPage, IDataOrder
    {
        public DataGridPage()
        {
            var request = HttpContext.Current.Request;
            int page, pageSize;

            int.TryParse(request["page"], out page);
            Page = page < 1 ? 1 : page;

            int.TryParse(request["rows"], out pageSize);
            PageSize = pageSize < 1 ? 1 : pageSize;
        }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }


        #region IDataPage Members

        public int Skip { get { return (Page - 1) * PageSize; } }

        public int PageSize { get; set; }

        public int RecordCount { get; set; }

        #endregion

        #region IDataOrder Members

        public string By { get; set; }

        public string Order { get; set; }

        #endregion
    }
}
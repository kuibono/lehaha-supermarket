using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Dto
{
    public class SearchDtoBase<T>
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public string sortField { get; set; }

        public string sortOrder { get; set; }

        public T entity { get; set; }
    }

    public class SearchDtoBaseTest
    {
        public int pageIndex { get; set; }

        public int pageSize { get; set; }

        public int sortField { get; set; }

        public int sortOrder { get; set; }

        public SysLoginPower po { get; set; }
    }
}

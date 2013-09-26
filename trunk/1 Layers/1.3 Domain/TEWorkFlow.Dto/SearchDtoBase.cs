using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Dto
{
    public class SearchDtoBase<T>
    {
        private int _pageindex = 0;
        public int pageIndex
        {
            get
            {
                if (_pageindex < 0)
                {
                    return 0;
                }
                return _pageindex;
            }
            set
            {
                _pageindex = value;
            }
        }

        private int _pagesize = 100;
        public int pageSize
        {
            get
            {
                if (_pagesize <= 0)
                {
                    return 1;
                }
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }

        public string sortField { get; set; }

        public string sortOrder { get; set; }

        public string key { get; set; }

        public T entity { get; set; }
    }

    public class SearchDtoBaseTest
    {
        private int _pageIndex = 1;
        public int pageIndex
        {
            get
            {
                if (_pageIndex <= 0)
                {
                    return 1;
                }
                return _pageIndex;
            }
            set
            {
                _pageIndex = value;
            }
        }

        private int _pagesize = 100;
        public int pageSize
        {
            get
            {
                if (_pagesize <= 0)
                {
                    return 1;
                }
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }

        public int sortField { get; set; }

        public int sortOrder { get; set; }

        public SysLoginPower po { get; set; }
    }
}

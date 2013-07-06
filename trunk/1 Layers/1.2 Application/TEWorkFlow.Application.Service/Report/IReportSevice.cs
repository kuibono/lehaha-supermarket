using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Report
{
    public interface IReportSevice
    {
        SearchResult<RetailPurchaseCompare> GetRetailPurchaseCompareList(DateTime dateS, DateTime dateE, SearchDtoBase<RetailPurchaseCompare> c);
    }
}

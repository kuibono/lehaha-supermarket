/*
*本代码由代码生成器自动生成，请不要更改此文件的任何代码。
*生成时间：2013/7/4 20:29:13
*生成者：kuibono
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;

namespace TEWorkFlow.Application.Service.Business
{
    public interface IRtRetailManageService
    {
        string Create(RtRetailManage entity);

        RtRetailManage GetById(string id);

        SearchResult<RtRetailManage> SearchReportBySupplier(DateTime? dateS, DateTime? dateE, string bCode, int pageSize = 20, int pageIndex = 1);

        IList<RtRetailManage> GetAll();

        void Update(RtRetailManage entity);

        /// <summary>
        /// 删除指定RtRetailManage
        /// </summary>
        /// <param name="entity"></param>
        void Delete(RtRetailManage entity);

        /// <summary>
        /// RtRetailManage
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<RtRetailManage> entitys);


        SearchResult<RtRetailManage> Search(SearchDtoBase<RtRetailManage> c);

        IList<RtRetailManage> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Application.Service.Sys
{
    public interface ISysPostService
    {
        string Create(SysPost entity);

        SysPost GetById(string id);

        IList<SysPost> GetAll(string supCode);

        void SetPostReaded(string id, string supCode);

        void Update(SysPost entity);

        /// <summary>
        /// 删除指定SysPost
        /// </summary>
        /// <param name="entity"></param>
        void Delete(SysPost entity);

        /// <summary>
        /// SysPost
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<SysPost> entitys);


        SearchResult<SysPost> Search(SearchDtoBase<SysPost> c);

        IList<SysPost> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
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
    public interface IBranchPostService
    {
        string Create(BranchPost entity);

        BranchPost GetById(string id);

        IList<BranchPost> GetAll(string supCode);

        void Update(BranchPost entity);

        /// <summary>
        /// 删除指定BranchPost
        /// </summary>
        /// <param name="entity"></param>
        void Delete(BranchPost entity);

        /// <summary>
        /// BranchPost
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<BranchPost> entitys);


        SearchResult<BranchPost> Search(SearchDtoBase<BranchPost> c);

        IList<BranchPost> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Archives
{
    public interface IBsBranchArchivesService
    {
        string Create(BsBranchArchives entity);

        BsBranchArchives GetById(string id);

        IList<BsBranchArchives> GetAll();

        void Update(BsBranchArchives entity);

        /// <summary>
        /// 删除指定BsBranchArchives
        /// </summary>
        /// <param name="entity"></param>
        void Delete(BsBranchArchives entity);

        /// <summary>
        /// BsBranchArchives
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<BsBranchArchives> entitys);


        SearchResult<BsBranchArchives> Search(SearchDtoBase<BsBranchArchives> c, string supCode = "");

        IList<BsBranchArchives> Search(string key, int pageSize = 20, int pageIndex = 1);

        IList<BsBranchArchives> Search(string key, string supCode, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
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


        SearchResult<BsBranchArchives> Search(SearchDtoBase<BsBranchArchives> c);

        void Delete(IList<string> ids);
    }
}
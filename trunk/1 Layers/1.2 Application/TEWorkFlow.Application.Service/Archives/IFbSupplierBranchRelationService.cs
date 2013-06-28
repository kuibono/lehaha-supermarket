using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Archives;

namespace TEWorkFlow.Application.Service.Archives
{
    public interface IFbSupplierBranchRelationService
    {
        IList<FbSupplierBranchRelation> GetAllRelationBySupplierCode(string supCode);

        IList<FbSupplierBranchRelation> GetAllRelationByBranchCode(string bCode);

        void SetValue(string bCode, string supCode, bool value);

        string Create(FbSupplierBranchRelation entity);

        FbSupplierBranchRelation GetById(string id);

        IList<FbSupplierBranchRelation> GetAll();

        void Update(FbSupplierBranchRelation entity);

        /// <summary>
        /// 删除指定FbSupplierBranchRelation
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbSupplierBranchRelation entity);

        /// <summary>
        /// FbSupplierBranchRelation
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbSupplierBranchRelation> entitys);


        SearchResult<FbSupplierBranchRelation> Search(SearchDtoBase<FbSupplierBranchRelation> c);

        IList<FbSupplierBranchRelation> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
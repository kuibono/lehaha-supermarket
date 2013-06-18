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
    public interface IFbGoodsSupplierRelationService
    {
        string Create(FbGoodsSupplierRelation entity);

        FbGoodsSupplierRelation GetById(string id);

        IList<FbGoodsSupplierRelation> GetAll();

        void Update(FbGoodsSupplierRelation entity);

        /// <summary>
        /// 删除指定FbGoodsSupplierRelation
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbGoodsSupplierRelation entity);

        /// <summary>
        /// FbGoodsSupplierRelation
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbGoodsSupplierRelation> entitys);


        SearchResult<FbGoodsSupplierRelation> Search(SearchDtoBase<FbGoodsSupplierRelation> c);

        IList<FbGoodsSupplierRelation> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}

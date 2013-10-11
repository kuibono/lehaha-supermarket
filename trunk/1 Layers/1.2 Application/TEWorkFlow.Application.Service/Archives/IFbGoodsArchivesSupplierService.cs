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
    public interface IFbGoodsArchivesSupplierService
    {
        string Create(FbGoodsArchivesSupplier entity);

        FbGoodsArchivesSupplier GetById(string id);

        IList<FbGoodsArchivesSupplier> GetAll();

        void Update(FbGoodsArchivesSupplier entity);

        /// <summary>
        /// 删除指定FbGoodsArchivesSupplier
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbGoodsArchivesSupplier entity);

        /// <summary>
        /// FbGoodsArchivesSupplier
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbGoodsArchivesSupplier> entitys);


        SearchResult<FbGoodsArchivesSupplier> Search(SearchDtoBase<FbGoodsArchivesSupplier> c);

        IList<FbGoodsArchivesSupplier> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);

        IList<FbGoodsArchivesSupplier> GetByGoodsCode(string goodsCode);

        IList<FbGoodsArchivesSupplier> GetBySupCode(string supCode);
    }
}
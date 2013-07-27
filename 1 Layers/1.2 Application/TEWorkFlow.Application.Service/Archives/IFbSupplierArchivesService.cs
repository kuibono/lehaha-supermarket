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
    public interface IFbSupplierArchivesService
    {
        string Create(FbSupplierArchives entity);

        FbSupplierArchives GetById(string id);

        IList<FbSupplierArchives> GetAll();

        void Update(FbSupplierArchives entity);

        /// <summary>
        /// 删除指定FbSupplierArchives
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbSupplierArchives entity);

        /// <summary>
        /// FbSupplierArchives
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbSupplierArchives> entitys);


        SearchResult<FbSupplierArchives> Search(SearchDtoBase<FbSupplierArchives> c);

        IList<FbSupplierArchives> Search(string key, int pageSize = 20, int pageIndex = 1);

        IList<FbSupplierArchives> SearchWithGoodsCount(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);

        string GenerateLoginName();

        string GenerateId();

    }
}
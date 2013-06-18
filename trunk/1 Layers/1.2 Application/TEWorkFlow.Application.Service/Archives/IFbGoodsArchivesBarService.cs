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
    public interface IFbGoodsArchivesBarService
    {
        string Create(FbGoodsArchivesBar entity);

        FbGoodsArchivesBar GetById(string id);

        IList<FbGoodsArchivesBar> GetAll();

        void Update(FbGoodsArchivesBar entity);

        /// <summary>
        /// 删除指定FbGoodsArchivesBar
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbGoodsArchivesBar entity);

        /// <summary>
        /// FbGoodsArchivesBar
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbGoodsArchivesBar> entitys);


        SearchResult<FbGoodsArchivesBar> Search(SearchDtoBase<FbGoodsArchivesBar> c);

        IList<FbGoodsArchivesBar> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}

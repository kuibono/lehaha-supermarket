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
    public interface IGoodsArchivesService
    {
        Result Create(GoodsArchives entity);

        GoodsArchives GetById(string id);

        IList<GoodsArchives> GetAll();

        Result Update(GoodsArchives entity);

        /// <summary>
        /// 删除指定GoodsArchives
        /// </summary>
        /// <param name="entity"></param>
        void Delete(GoodsArchives entity);

        /// <summary>
        /// GoodsArchives
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<GoodsArchives> entitys);


        SearchResult<GoodsArchives> Search(SearchDtoBase<GoodsArchives> c);

        IList<GoodsArchives> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);

        string GenarateGbCode();

        string GenarateId(GoodsArchives entity);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Category;

namespace TEWorkFlow.Application.Service.Category
{
    public interface IFbPaGoodsGsService
    {
        string Create(FbPaGoodsGs entity);

        FbPaGoodsGs GetById(string id);

        IList<FbPaGoodsGs> GetAll();

        void Save(FbPaGoodsGs entity);

        IList<FbPaGoodsGs> GetByGbId(string GmId);

        void Update(FbPaGoodsGs entity);

        /// <summary>
        /// 删除指定FbPaGoodsGs
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbPaGoodsGs entity);

        /// <summary>
        /// FbPaGoodsGs
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbPaGoodsGs> entitys);


        SearchResult<FbPaGoodsGs> Search(SearchDtoBase<FbPaGoodsGs> c);

        void Delete(IList<string> ids);

        string GenarateId(string gmCode);

    }
}
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
    public interface IFbPaGoodsGmService
    {
        string Create(FbPaGoodsGm entity);

        FbPaGoodsGm GetById(string id);

        IList<FbPaGoodsGm> GetAll();

        void Save(FbPaGoodsGm entity);

        IList<FbPaGoodsGm> GetByGbId(string GbId);

        void Update(FbPaGoodsGm entity);

        /// <summary>
        /// 删除指定FbPaGoodsGm
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbPaGoodsGm entity);

        /// <summary>
        /// FbPaGoodsGm
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbPaGoodsGm> entitys);


        SearchResult<FbPaGoodsGm> Search(SearchDtoBase<FbPaGoodsGm> c);

        void Delete(IList<string> ids);

        string GenarateId(string gbCode);
    }
}
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
    public interface IFbPaGoodsGlService
    {
        string Create(FbPaGoodsGl entity);

        FbPaGoodsGl GetById(string id);

        IList<FbPaGoodsGl> GetAll();

        void Save(FbPaGoodsGl entity);

        IList<FbPaGoodsGl> GetByGsId(string GsId);

        void Update(FbPaGoodsGl entity);

        /// <summary>
        /// 删除指定FbPaGoodsGl
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbPaGoodsGl entity);

        /// <summary>
        /// FbPaGoodsGl
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbPaGoodsGl> entitys);


        SearchResult<FbPaGoodsGl> Search(SearchDtoBase<FbPaGoodsGl> c);

        void Delete(IList<string> ids);

        string GenarateId(string gsCode);
    }
}
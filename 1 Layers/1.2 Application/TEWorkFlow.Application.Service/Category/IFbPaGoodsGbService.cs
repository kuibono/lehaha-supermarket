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
    public interface IFbPaGoodsGbService
    {
        string Create(FbPaGoodsGb entity);

        FbPaGoodsGb GetById(string id);

        IList<FbPaGoodsGb> GetAll();

        void Save(FbPaGoodsGb entity);

        void Update(FbPaGoodsGb entity);

        /// <summary>
        /// 删除指定FbPaGoodsGb
        /// </summary>
        /// <param name="entity"></param>
        void Delete(FbPaGoodsGb entity);

        /// <summary>
        /// FbPaGoodsGb
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<FbPaGoodsGb> entitys);


        SearchResult<FbPaGoodsGb> Search(SearchDtoBase<FbPaGoodsGb> c);

        void Delete(IList<string> ids);

        string GenarateId();
    }
}
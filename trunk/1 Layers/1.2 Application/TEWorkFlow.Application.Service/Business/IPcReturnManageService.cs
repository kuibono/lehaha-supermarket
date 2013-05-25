using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;

namespace TEWorkFlow.Application.Service.Business
{
    public interface IPcReturnManageService
    {
        string Create(PcReturnManage entity);

        PcReturnManage GetById(string id);

        IList<PcReturnManage> GetAll();

        void Update(PcReturnManage entity);

        void Save(PcReturnManage entity);

        /// <summary>
        /// 删除指定PcReturnManage
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcReturnManage entity);

        /// <summary>
        /// PcReturnManage
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcReturnManage> entitys);


        SearchResult<PcReturnManage> Search(string SupplierId, SearchDtoBase<PcReturnManage> c);

        IList<PcReturnManage> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
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
    public interface IPcSupplementManageService
    {
        string Create(PcSupplementManage entity);

        PcSupplementManage GetById(string id);

        void Save(PcSupplementManage entity);

        IList<PcSupplementManage> GetAll();

        void Update(PcSupplementManage entity);

        /// <summary>
        /// 删除指定PcSupplementManage
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcSupplementManage entity);

        /// <summary>
        /// PcSupplementManage
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcSupplementManage> entitys);


        SearchResult<PcSupplementManage> Search(SearchDtoBase<PcSupplementManage> c);

        IList<PcSupplementManage> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
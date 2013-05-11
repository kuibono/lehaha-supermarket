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
    public interface IPcSupplementDetailService
    {
        string Create(PcSupplementDetail entity);

        PcSupplementDetail GetById(string id);

        IList<PcSupplementDetail> GetAll();



        void Update(PcSupplementDetail entity);

        /// <summary>
        /// 删除指定PcSupplementDetail
        /// </summary>
        /// <param name="entity"></param>
        void Delete(PcSupplementDetail entity);

        /// <summary>
        /// PcSupplementDetail
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<PcSupplementDetail> entitys);

        IList<PcSupplementDetail> GetDetailsByManageId(string manageId);

        SearchResult<PcSupplementDetail> Search(SearchDtoBase<PcSupplementDetail> c);

        void Delete(IList<string> ids);
    }
}
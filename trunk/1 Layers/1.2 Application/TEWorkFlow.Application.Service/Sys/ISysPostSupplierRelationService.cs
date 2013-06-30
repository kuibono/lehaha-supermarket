using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Sys;

namespace TEWorkFlow.Application.Service.Sys
{
    public interface ISysPostSupplierRelationService
    {
        string Create(SysPostSupplierRelation entity);

        SysPostSupplierRelation GetById(string id);

        IList<SysPostSupplierRelation> GetAll();

        void Update(SysPostSupplierRelation entity);

        /// <summary>
        /// 删除指定SysPostSupplierRelation
        /// </summary>
        /// <param name="entity"></param>
        void Delete(SysPostSupplierRelation entity);

        /// <summary>
        /// SysPostSupplierRelation
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<SysPostSupplierRelation> entitys);


        SearchResult<SysPostSupplierRelation> Search(SearchDtoBase<SysPostSupplierRelation> c);

        IList<SysPostSupplierRelation> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);
    }
}
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
    public interface ISysPaDepartmentService
    {
        string Create(SysPaDepartment entity);

        SysPaDepartment GetById(string id);

        IList<SysPaDepartment> GetAll();

        void Update(SysPaDepartment entity);

        /// <summary>
        /// 删除指定SysPaDepartment
        /// </summary>
        /// <param name="entity"></param>
        void Delete(SysPaDepartment entity);

        /// <summary>
        /// SysPaDepartment
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<SysPaDepartment> entitys);


        SearchResult<SysPaDepartment> Search(SearchDtoBase<SysPaDepartment> c);

        IList<SysPaDepartment> Search(string key, int pageSize = 20, int pageIndex = 1);

        void Delete(IList<string> ids);

        IList<SysTreeNodeDto> ModelListToDto(IList<SysPaDepartment> entitys);
    }
}
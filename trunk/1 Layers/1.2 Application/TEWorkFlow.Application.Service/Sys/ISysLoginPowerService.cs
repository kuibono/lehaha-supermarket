using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Sys
{
    public interface ISysLoginPowerService
    {
        string Create(SysLoginPower entity);

        SysLoginPower GetById(string id);

        IList<SysLoginPower> GetAll();

        void Update(SysLoginPower entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Delete(SysLoginPower entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<SysLoginPower> entitys);

        SysLoginPower CheckUser(string UserName, string Password);

        LoginResult CheckUser(string UserName, string Password, int UserType);

        IList<SysLoginPower> GetList(ISpecification<SysLoginPower> specification, Action<Orderable<SysLoginPower>> order);

        SearchResult<SysLoginPower> Search(SearchDtoBase<SysLoginPower> c);

        void Delete(IList<string> ids);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Sys
{
    public interface ISysmodulecontentService
    {
        string Create(Sysmodulecontent entity);

        Sysmodulecontent GetById(string id);

        IList<Sysmodulecontent> GetAll();

        void Update(Sysmodulecontent entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entity"></param>
        void Delete(Sysmodulecontent entity);

        /// <summary>
        /// 删除指定WorkFlow
        /// </summary>
        /// <param name="entitys"></param>
        void Delete(IEnumerable<Sysmodulecontent> entitys);

        IList<SysTreeNodeDto> ModelListToDto(IList<Sysmodulecontent> entitys);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Dto;

namespace TEWorkFlow.Application.Service.Sys
{
    public class SysmodulecontentService : ISysmodulecontentService
    {
        public IRepositoryGUID<Sysmodulecontent> SysmodulecontentRepository { get; set; }

        [Transaction]
        public string Create(Sysmodulecontent entity)
        {
            return SysmodulecontentRepository.Save(entity);
        }

        [Transaction]
        public Sysmodulecontent GetById(string id)
        {
            return SysmodulecontentRepository.Get(id);
        }

        [Transaction]
        public IList<Sysmodulecontent> GetAll()
        {
            return SysmodulecontentRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Update(Sysmodulecontent entity)
        {
            SysmodulecontentRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Sysmodulecontent entity)
        {
            SysmodulecontentRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Sysmodulecontent> entitys)
        {
            foreach (var entity in entitys)
            {
                SysmodulecontentRepository.Delete(entity);
            }
        }

        [Transaction]
        public IList<SysTreeNodeDto> ModelListToDto(IList<Sysmodulecontent>  entitys)
        {
            IList<SysTreeNodeDto> result = new List<SysTreeNodeDto>();
            foreach (var entity in entitys)
            {
                result.Add(new SysTreeNodeDto()
                               {
                                   id = entity.Id,
                                   iconCls = entity.Icon,
                                   pid=entity.ParentId,
                                   text = entity.Windowname,
                                   url = entity.Url,
                                   newwindow=entity.OpenInNewWindow
                               });
            }
            return result;
        }
    }
}

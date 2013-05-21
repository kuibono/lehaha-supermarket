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
    public class SysEnterpriseArchivesService : ISysEnterpriseArchivesService
    {

        public IRepositoryGUID<SysEnterpriseArchives> EntityRepository { get; set; }

        [Transaction]
        public SysEnterpriseArchives Get()
        {
            if (NSH.VSTO.Cache.GetCache("SysSetting") == null)
            {
                SysEnterpriseArchives item = new SysEnterpriseArchives();
                if (EntityRepository.LinqQuery.Count() != 0)
                {

                    item = EntityRepository.LinqQuery.First();
                }
                NSH.VSTO.Cache.SetCache("SysSetting", item);
            }
            return (SysEnterpriseArchives)NSH.VSTO.Cache.GetCache("SysSetting");
        }

        [Transaction]
        public void Save(SysEnterpriseArchives entity)
        {
            var q = from l in EntityRepository.LinqQuery where l.Id == entity.Id select l;
            if (q.Count() == 0)
            {
                //不存在
                EntityRepository.Save(entity);
            }
            else
            {
                EntityRepository.Update(entity);
            }
            NSH.VSTO.Cache.Clear("SysSetting");
        }
    }
}

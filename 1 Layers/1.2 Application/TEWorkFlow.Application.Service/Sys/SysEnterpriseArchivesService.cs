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
            if (EntityRepository.LinqQuery.Count() == 0)
            {
                return new SysEnterpriseArchives();
            }
            else
            {
                return EntityRepository.LinqQuery.First();
            }
        }

        [Transaction]
        public void Save(SysEnterpriseArchives entity)
        {
            var q = from l in EntityRepository.LinqQuery where l.Id == entity.Id select l;
            if (q.Count() == 0)
            {
                //不存在
                EntityRepository.Update(entity);
            }
            else
            {
                EntityRepository.Save(entity);
            }
        }
    }
}

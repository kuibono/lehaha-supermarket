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
    public class FbPaBaseSetService : IFbPaBaseSetService
    {

        public IRepositoryGUID<FbPaBaseSet> EntityRepository { get; set; }
        [Transaction]
        public FbPaBaseSet Get()
        {
            if (EntityRepository.LinqQuery.Count() == 0)
            {
                return new FbPaBaseSet();
            }
            else
            {
                return EntityRepository.LinqQuery.First();
            }
        }

        [Transaction]
        public void Save(FbPaBaseSet entity)
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
        }
    }
}

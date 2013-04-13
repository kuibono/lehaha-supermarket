using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TEWorkFlow.Domain.WorkFlow;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using NSH.Authorization.Domain;

namespace TEWorkFlow.Application.Service.WorkFlow
{
    public class CheckPointService : ICheckPointService
    {
        public IRepository<CheckPoint> CheckPointRepository { get; set; }

        [Transaction]
        public int Create(CheckPoint entity)
        {
            return CheckPointRepository.Save(entity);
        }

        [Transaction(ReadOnly = true)]
        public CheckPoint GetById(int id)
        {
            return CheckPointRepository.Get(id);
        }

        [Transaction(ReadOnly = true)]
        public IList<CheckPoint> GetAll()
        {
            return CheckPointRepository.LinqQuery.ToList();
        }

        [Transaction(ReadOnly = true)]
        public IList<CheckPoint> GetListByWorkstep(WorkStep workstep)
        {
            var query = CheckPointRepository.LinqQuery;
            if (workstep.Id > 0)
            {
                query = query.Where(o => o.Step.Id == workstep.Id || o.IsKey);
            }
            return query.ToList();
        }

        [Transaction(ReadOnly = true)]
        public IList<CheckPoint> GetListByUser(User user, bool? iskey = false)
        {
            var query = CheckPointRepository.LinqQuery;
            query = query.Where(o => o.Step.Users.Any(u => u.Id == user.Id));
            if (iskey.HasValue)
            {
                query = query.Where(o => o.IsKey == iskey.Value);
            }
            return query.ToList();
        }

        [Transaction]
        public void Update(CheckPoint entity)
        {
            CheckPointRepository.Update(entity);
        }

        [Transaction]
        public void Delete(CheckPoint entity)
        {
            CheckPointRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<CheckPoint> entitys)
        {
            foreach (CheckPoint entity in entitys)
            {
                CheckPointRepository.Delete(entity);
            }
        }
    }
}

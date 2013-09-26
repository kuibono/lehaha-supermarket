using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Domain.Archives;

namespace TEWorkFlow.Application.Service.Business
{
    public class PcReturnManageService : IPcReturnManageService
    {

        public IRepositoryGUID<PcReturnManage> EntityRepository { get; set; }
        public IRepositoryGUID<SysPaDepartment> DepartmentRepository { get; set; }
        public IRepositoryGUID<BsBranchArchives> BranchRepository { get; set; }
        [Transaction]
        private void FillDepartmentName(IList<PcReturnManage> manages)
        {
            var departments = DepartmentRepository.LinqQuery.ToList();
            for (int i = 0; i < manages.Count; i++)
            {
                string depId = manages[i].dCode;
                string depName = departments.Where(p => p.Id == depId).Count() > 0 ? departments.Where(p => p.Id == depId).First().dName : "";
                manages[i].dName = depName;
            }
        }
        private void FillDepartmentName(PcReturnManage manage)
        {
            var departments = DepartmentRepository.LinqQuery.ToList();
            string depName = departments.Where(p => p.Id == manage.dCode).Count() > 0 ? departments.Where(p => p.Id == manage.dCode).First().dName : "";
            manage.dName = depName;
        }
        private void FillBranchName(IList<PcReturnManage> manages)
        {
            var branchs = BranchRepository.LinqQuery.ToList();
            for (int i = 0; i < manages.Count; i++)
            {
                string id = manages[i].bCode;
                string name = branchs.Where(p => p.Id == id).Count() > 0 ? branchs.Where(p => p.Id == id).First().bName : "";
                manages[i].bName = name;
            }
        }
        [Transaction]
        public string Create(PcReturnManage entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public void Save(PcReturnManage entity)
        {
            bool add=false;
            if (string.IsNullOrEmpty(entity.Id))
            {
                add = true;
                entity.Id = Guid.NewGuid().ToString();
            }
            else
            {
                if (EntityRepository.LinqQuery.Count(p => p.Id == entity.Id) > 0)
                {
                    add = false;
                }
                else
                {
                    add = true;
                }
            }

            if (add)
            {
                EntityRepository.Save(entity);
            }
            else
            {
                EntityRepository.Update(entity);
            }
        }

        [Transaction]
        public PcReturnManage GetById(string id)
        {
            var result=EntityRepository.Get(id);
            return result;
        }

        [Transaction]
        public IList<PcReturnManage> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(PcReturnManage entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(PcReturnManage entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<PcReturnManage> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<PcReturnManage> Search(string SupplierId, SearchDtoBase<PcReturnManage> c)
        {
            var q = EntityRepository.LinqQuery.Where(p=>p.EnCode==SupplierId);
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.Id) == false)
                {
                    q = q.Where(p => p.Id.Contains(c.entity.Id));
                }
                if (string.IsNullOrEmpty(c.entity.PcNumber) == false)
                {
                    q = q.Where(p => p.PcNumber.Contains(c.entity.PcNumber));
                }
                if (string.IsNullOrEmpty(c.entity.SupCode) == false)
                {
                    q = q.Where(p => p.SupCode.Contains(c.entity.SupCode));
                }
                if (string.IsNullOrEmpty(c.entity.EnCode) == false)
                {
                    q = q.Where(p => p.EnCode.Contains(c.entity.EnCode));
                }
                if (string.IsNullOrEmpty(c.entity.PcForm) == false)
                {
                    q = q.Where(p => p.PcForm.Contains(c.entity.PcForm));
                }
                if (string.IsNullOrEmpty(c.entity.dCode) == false)
                {
                    q = q.Where(p => p.dCode.Contains(c.entity.dCode));
                }
                if (string.IsNullOrEmpty(c.entity.bCode) == false)
                {
                    q = q.Where(p => p.bCode.Contains(c.entity.bCode));
                }
                if (string.IsNullOrEmpty(c.entity.WhCode) == false)
                {
                    q = q.Where(p => p.WhCode.Contains(c.entity.WhCode));
                }
                if (string.IsNullOrEmpty(c.entity.IfPutout) == false)
                {
                    q = q.Where(p => p.IfPutout.Contains(c.entity.IfPutout));
                }
                if (string.IsNullOrEmpty(c.entity.Operator) == false)
                {
                    q = q.Where(p => p.Operator.Contains(c.entity.Operator));
                }
                if (string.IsNullOrEmpty(c.entity.Assessor) == false)
                {
                    q = q.Where(p => p.Assessor.Contains(c.entity.Assessor));
                }
                if (string.IsNullOrEmpty(c.entity.IfExamine) == false)
                {
                    q = q.Where(p => p.IfExamine.Contains(c.entity.IfExamine));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            FillDepartmentName(result);
            FillBranchName(result);
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<PcReturnManage> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.Id.Contains(key)
                    || l.PcNumber.Contains(key)
                    || l.SupCode.Contains(key)
                    || l.EnCode.Contains(key)
                    || l.PcForm.Contains(key)
                    || l.dCode.Contains(key)
                    || l.bCode.Contains(key)
                    || l.WhCode.Contains(key)
                    || l.IfPutout.Contains(key)
                    || l.Operator.Contains(key)
                    || l.Assessor.Contains(key)
                    || l.IfExamine.Contains(key)
                    select l;


            }
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillDepartmentName(result);
            FillBranchName(result);
            return result.ToList();
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = EntityRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (var each in q)
            {
                Delete(each);
            }
        }
    }
}

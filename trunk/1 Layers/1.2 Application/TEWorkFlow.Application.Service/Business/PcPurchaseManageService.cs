using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Business;

namespace TEWorkFlow.Application.Service.Business
{
    public class PcPurchaseManageService : IPcPurchaseManageService
    {

        public IRepositoryGUID<PcPurchaseManage> EntityRepository { get; set; }
        public IRepositoryGUID<PcPurchaseDetail> DetailRepository { get; set; }
        [Transaction]
        public string Create(PcPurchaseManage entity)
        {
            string id = EntityRepository.Save(entity);

            if (entity.Detail == null)
            {
                entity.Detail = new PcPurchaseDetail();
                entity.Detail.Id = id;
            }
            return DetailRepository.Save(entity.Detail);
            
        }

        [Transaction]
        public PcPurchaseManage GetById(string id)
        {
            var entity = EntityRepository.Get(id);
            entity.Detail = DetailRepository.Get(id);
            if (entity.Detail == null)
            {
                entity.Detail = new PcPurchaseDetail();
                entity.Detail.Id = id;
            }
            return entity;
        }

        [Transaction]
        public IList<PcPurchaseManage> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(PcPurchaseManage entity)
        {
            EntityRepository.Update(entity);
            if (entity.Detail != null)
            {
                DetailRepository.Update(entity.Detail);
            }
        }

        [Transaction]
        public void Delete(PcPurchaseManage entity)
        {
            EntityRepository.Delete(entity);
            if (entity.Detail != null)
            {
                DetailRepository.Delete(entity.Detail);
            }
        }

        [Transaction]
        public void Delete(IEnumerable<PcPurchaseManage> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
                if (entity.Detail != null)
                {
                    DetailRepository.Delete(entity.Detail);
                }
            }
        }


        [Transaction]
        public SearchResult<PcPurchaseManage> Search(SearchDtoBase<PcPurchaseManage> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

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
                if (string.IsNullOrEmpty(c.entity.PcType) == false)
                {
                    q = q.Where(p => p.PcType.Contains(c.entity.PcType));
                }
                if (string.IsNullOrEmpty(c.entity.PcMode) == false)
                {
                    q = q.Where(p => p.PcMode.Contains(c.entity.PcMode));
                }
                if (string.IsNullOrEmpty(c.entity.WhCode) == false)
                {
                    q = q.Where(p => p.WhCode.Contains(c.entity.WhCode));
                }
                if (string.IsNullOrEmpty(c.entity.IfCheck) == false)
                {
                    q = q.Where(p => p.IfCheck.Contains(c.entity.IfCheck));
                }
                if (string.IsNullOrEmpty(c.entity.IfPutin) == false)
                {
                    q = q.Where(p => p.IfPutin.Contains(c.entity.IfPutin));
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

            q = q.Skip((c.pageIndex - 1) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = EntityRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (var each in q)
            {
                Delete(each);
                if (each.Detail != null)
                {
                    DetailRepository.Delete(each.Detail);
                }
            }
        }
    }
}

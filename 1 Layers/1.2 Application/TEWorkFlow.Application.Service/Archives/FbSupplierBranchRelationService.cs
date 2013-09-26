using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Application.Service.Sys;

namespace TEWorkFlow.Application.Service.Archives
{
    public class FbSupplierBranchRelationService : IFbSupplierBranchRelationService
    {

        public IRepositoryGUID<FbSupplierBranchRelation> EntityRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> SupplierRepository { get; set; }
        public IRepositoryGUID<BsBranchArchives> BsBranchArchivesRepository { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }

        [Transaction]
        public IList<FbSupplierBranchRelation> GetAllRelationBySupplierCode(string supCode)
        {
            FbSupplierArchives sup = SupplierRepository.Get(supCode);

            IList<BsBranchArchives> allBranchs = BsBranchArchivesRepository.LinqQuery.Where(p => p.IfExamine == "1").ToList();
            IList<FbSupplierBranchRelation> allRelation = EntityRepository.LinqQuery.Where(p => p.SupCode == supCode).ToList();
            foreach (BsBranchArchives branch in allBranchs)
            {
                if (!allRelation.Any(p => p.bCode == branch.Id))
                {
                    FbSupplierBranchRelation relation = new FbSupplierBranchRelation()
                    {
                        Available = false,
                        bCode = branch.Id,
                        bName=branch.bName,
                        SupCode = supCode,
                        SupName=sup.SupName,
                        Id = Guid.NewGuid().ToString()
                    };
                    EntityRepository.Save(relation);
                }
            }
            return EntityRepository.LinqQuery.Where(p => p.SupCode == supCode).ToList();
        }

        [Transaction]
        public IList<FbSupplierBranchRelation> GetAllRelationByBranchCode(string bCode)
        {
            BsBranchArchives branch = BsBranchArchivesRepository.Get(bCode);
            IList<FbSupplierArchives> allSuppliers = SupplierRepository.LinqQuery.Where(p => p.IfExamine == "1" || p.IfExamine == "true").ToList();
            IList<FbSupplierBranchRelation> allRelation = EntityRepository.LinqQuery.Where(p => p.bCode == bCode).ToList();
            foreach (FbSupplierArchives sup in allSuppliers)
            {
                if (!allRelation.Any(p => p.SupCode == sup.Id))
                {
                    FbSupplierBranchRelation relation = new FbSupplierBranchRelation()
                    {
                        Available = false,
                        bCode =bCode,
                        bName=branch.bName,
                        SupCode = sup.Id,
                        SupName=sup.SupName,
                        Id = Guid.NewGuid().ToString()
                    };
                    EntityRepository.Save(relation);
                }
            }
            return EntityRepository.LinqQuery.Where(p => p.bCode==bCode).ToList();
        }

        [Transaction]
        public void SetValue(string bCode, string supCode, bool value)
        {
            if (!EntityRepository.LinqQuery.Any(p => p.SupCode == supCode && p.bCode == bCode))
            {
                FbSupplierArchives sup = SupplierRepository.Get(supCode);
                BsBranchArchives branch = BsBranchArchivesRepository.Get(bCode);
                FbSupplierBranchRelation relation = new FbSupplierBranchRelation();
                relation.Available = value;
                relation.bCode = bCode;
                relation.bName = branch.bName;
                relation.Id = Guid.NewGuid().ToString();
                relation.SupCode = supCode;
                relation.SupName = sup.SupName;
                EntityRepository.Save(relation);
                TfDataDownloadService.AddDownload("fb_supplier_branch_relation", relation.Id);
            }
            else
            {
                FbSupplierBranchRelation relation = EntityRepository.LinqQuery.Where(p => p.SupCode == supCode && p.bCode == bCode).First();
                relation.Available = value;
                EntityRepository.Update(relation);
                TfDataDownloadService.AddDownload("fb_supplier_branch_relation", relation.Id);
            }
        }

        [Transaction]
        public string Create(FbSupplierBranchRelation entity)
        {
            return EntityRepository.Save(entity);
        }

        [Transaction]
        public FbSupplierBranchRelation GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<FbSupplierBranchRelation> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(FbSupplierBranchRelation entity)
        {
            EntityRepository.Update(entity);
        }

        [Transaction]
        public void Delete(FbSupplierBranchRelation entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbSupplierBranchRelation> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbSupplierBranchRelation> Search(SearchDtoBase<FbSupplierBranchRelation> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.SupCode) == false)
                {
                    q = q.Where(p => p.SupCode.Contains(c.entity.SupCode));
                }
                if (string.IsNullOrEmpty(c.entity.bCode) == false)
                {
                    q = q.Where(p => p.bCode.Contains(c.entity.bCode));
                }

            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<FbSupplierBranchRelation> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.Id.Contains(key)
                    || l.SupCode.Contains(key)
                    || l.bCode.Contains(key)
                    select l;


            }
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
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
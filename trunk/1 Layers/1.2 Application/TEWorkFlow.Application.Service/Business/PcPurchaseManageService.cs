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
using TEWorkFlow.Application.Service.Archives;

namespace TEWorkFlow.Application.Service.Business
{
    public class PcPurchaseManageService : IPcPurchaseManageService
    {

        public IRepositoryGUID<PcPurchaseManage> EntityRepository { get; set; }
        public IRepositoryGUID<PcPurchaseDetail> DetailRepository { get; set; }
        public IRepositoryGUID<SysPaDepartment> DepartmentRepository { get; set; }
        public IRepositoryGUID<BsBranchArchives> BranchRepository { get; set; }
        public IRepositoryGUID<PcPurchaseDetail> PurchaseDetailRepository { get; set; }
        public IRepositoryGUID<GoodsArchives> GoodsArchivesRepository { get; set; }
        public IEmemployeearchiveService EmemployeearchiveService { get; set; }
        [Transaction]
        public string Create(PcPurchaseManage entity)
        {
            return EntityRepository.Save(entity);

        }
        [Transaction]
        public void Save(PcPurchaseManage entity)
        {
            bool add = false;
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
            UpdatePurchaseAmount(entity.Id);
        }
        [Transaction]
        public PcPurchaseManage GetById(string id)
        {
            var entity = EntityRepository.Get(id);
            if (entity != null)
            {
                FillPurchaseInfo(entity);
            }
            return entity;
        }

        private void FillPurchaseInfo(PcPurchaseManage entity)
        {
            var branchs = BranchRepository.LinqQuery.ToList();
            var q = from l in branchs where l.Id == entity.bCode select l;
            if (q.Count() > 0)
            {
                entity.bName = q.First().bName;
            }

            var users = EmemployeearchiveService.GetAll();
            var q_u = from l in users where l.Id == entity.Operator select l;
            if (q_u.Count() > 0)
            {
                entity.operatorName = q_u.First().LoginName;
            }
        }

        [Transaction]
        private void FillDepartmentName(IList<PcPurchaseManage> manages)
        {
            var departments = DepartmentRepository.LinqQuery.ToList();
            for (int i = 0; i < manages.Count; i++)
            {
                string depId = manages[i].dCode;
                string depName = departments.Where(p => p.Id == depId).Count() > 0 ? departments.Where(p => p.Id == depId).First().dName : "";
                manages[i].dName = depName;
            }
        }

        private void FillBranchName(IList<PcPurchaseManage> manages)
        {
            var branchs = BranchRepository.LinqQuery.ToList();
            for (int i = 0; i < manages.Count; i++)
            {
                string id = manages[i].bCode;
                string name = branchs.Where(p => p.Id == id).Count() > 0 ? branchs.Where(p => p.Id == id).First().bName : "";
                manages[i].bName = name;
            }
        }

        private void FillDepartmentName(PcPurchaseManage manage)
        {
            var departments = DepartmentRepository.LinqQuery.ToList();
            string depName = departments.Where(p => p.Id == manage.dCode).Count() > 0 ? departments.Where(p => p.Id == manage.dCode).First().dName : "";
            manage.dName = depName;
        }

        [Transaction]
        public void UpdatePurchaseAmount(string id)
        {
            PcPurchaseManage entity = EntityRepository.Get(id);
            if (entity == null)
                return;

            var amount = PurchaseDetailRepository.LinqQuery.Where(p => p.ManageId == entity.Id).Sum(p => p.PurchaseMoney);
            entity.amount = amount;

            EntityRepository.Update(entity);
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
            UpdatePurchaseAmount(entity.Id);
        }

        [Transaction]
        public void Delete(PcPurchaseManage entity)
        {
            EntityRepository.Delete(entity);

            //删除下面的明细
            var details = DetailRepository.LinqQuery.Where(p => p.ManageId == entity.Id);
            foreach (var detail in details)
            {
                DetailRepository.Delete(detail);
            }
        }

        [Transaction]
        public void Delete(IEnumerable<PcPurchaseManage> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<PcPurchaseManage> Search(string SupplierId, SearchDtoBase<PcPurchaseManage> c, DateTime? dates, DateTime? dateE)
        {
            var q = EntityRepository.LinqQuery.Where(p=>p.EnCode==SupplierId);
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.Id) == false)
                {
                    q = q.Where(p => p.Id.Contains(c.entity.Id));
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
                    q = q.Where(p => p.bCode.Equals(c.entity.bCode));
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
            if (dateE != null && dates != null)
            {
                q = q.Where(p => p.PurchaseDate < dateE && p.PurchaseDate > dates);
            }
            int count = q.Count();

            q = q.OrderByDescending(p => p.Id).Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            FillDepartmentName(result);
            FillBranchName(result);
            return result.ToSearchResult(count);
        }


        public SearchResult<PcPurchaseManage> Search(DateTime? dateS, DateTime? dateE, string Encode, int pageSize = 20, int pageIndex = 1)
        {
            var q = from l in EntityRepository.LinqQuery where l.IfExamine.ToLower() == "1" select l;
            if (dateS == null || dateE == null)
            {
                q = from l in q
                    where l.EnCode == Encode
                    select l;
            }
            if (dateS <= dateE && string.IsNullOrEmpty(Encode) == false)
            {
                q = from l in q
                    where l.PurchaseDate >= dateS
                        && l.PurchaseDate <= dateE
                        && l.EnCode == Encode
                    select l;
            }
            int count = q.Count();
            q = q.OrderByDescending(p=>p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillDepartmentName(result);
            FillBranchName(result);
            return result.ToSearchResult(count);
        }

        public SearchResult<PcPurchaseManage> SearchReportByBranch(DateTime? dateS, DateTime? dateE, string BranchId, int pageSize = 20, int pageIndex = 1)
        {
            var q = from l in EntityRepository.LinqQuery where l.IfExamine.ToLower() == "1" select l;
            if (dateS == null || dateE == null)
            {
                q = from l in q
                    where l.bCode == BranchId
                    select l;
            }
            if (dateS <= dateE && string.IsNullOrEmpty(BranchId) == false)
            {
                q = from l in q
                    where l.PurchaseDate >= dateS
                        && l.PurchaseDate <= dateE
                        && l.bCode == BranchId
                    select l;
            }
            int count = q.Count();

            var result = q.OrderByDescending(p => p.Id).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var statics = PurchaseDetailRepository.LinqQuery.Where(p => p.ManageId == result[i].Id);
                result[i].detailCount = statics.Count();
                result[i].count = Convert.ToInt32(statics.Sum(p => p.PurchaseQty));
                result[i].amount = statics.Sum(p => p.PurchaseMoney);
            }

            var sta = new
            {
                count = result.Count,
                detailCount = result.Sum(p => p.detailCount),
                goodscount = result.Sum(p => p.count),
                amount = result.Sum(p => p.amount)
            };

            var searchResult = result.ToSearchResult(count);
            searchResult.Statistics = sta;
            return searchResult;

        }


        public SearchResult<PcPurchaseManage> SearchReportBySupplier(DateTime? dateS, DateTime? dateE, string SupCode,string bCode, int pageSize = 20, int pageIndex = 1)
        {
            var q = from l in EntityRepository.LinqQuery where l.IfExamine.ToLower() == "1" select l;
            if (dateS == null || dateE == null)
            {
                q = from l in q
                    where l.EnCode == SupCode
                    select l;
            }
            if (dateS <= dateE && string.IsNullOrEmpty(SupCode) == false)
            {
                q = from l in q
                    where l.PurchaseDate >= dateS
                        && l.PurchaseDate <= dateE
                        && l.EnCode == SupCode
                    select l;
            }
            if (string.IsNullOrEmpty(bCode)==false)
            {
                q = from l in q where l.bCode == bCode select l;
            }
            int count = q.Count();

            var result = q.OrderByDescending(p=>p.Id).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var statics = PurchaseDetailRepository.LinqQuery.Where(p => p.ManageId == result[i].Id);
                result[i].detailCount = statics.Count();
                result[i].count = Convert.ToInt32(statics.Sum(p => p.PurchaseQty));
                result[i].amount = statics.Sum(p => p.PurchaseMoney);
            }

            var sta = new
            {
                count = result.Count,
                detailCount = result.Sum(p => p.detailCount),
                goodscount = result.Sum(p => p.count),
                amount = result.Sum(p => p.amount)
            };
            FillBranchName(result);
            var searchResult = result.ToSearchResult(count);
            searchResult.Statistics = sta;
            //FillBranchName(result);
            return searchResult;

        }

        public SearchResult<PurchaseGoodsResult> SearchForPurchaseGoods(DateTime? dateS, DateTime? dateE, string BranchId, string SupCode, int pageSize = 20, int pageIndex = 1)
        {
            var q = from l in EntityRepository.LinqQuery where l.IfExamine.ToLower() == "1" select l;
            if (dateS == null || dateE == null)
            {
                q = from l in q
                    where l.bCode == BranchId
                    select l;
            }
            if (dateS <= dateE && string.IsNullOrEmpty(BranchId) == false)
            {
                q = from l in q
                    where l.PurchaseDate >= dateS
                        && l.PurchaseDate <= dateE
                        && l.bCode == BranchId
                    select l;
            }
            if (string.IsNullOrEmpty(SupCode)==false)
            {
                q = from l in q where l.EnCode == SupCode select l;
            }
            var manages = q.OrderByDescending(p => p.Id).ToList();
            var manageIds = manages.Select(p => p.Id).ToArray();

            var details = (from l in PurchaseDetailRepository.LinqQuery
                           where
                               manageIds.Contains(l.ManageId)
                           select l).ToList();
            var detailGoodsIds=details.Select(p=>p.GoodsCode).ToList().ToArray();
            var goods = GoodsArchivesRepository.LinqQuery.Where(p => detailGoodsIds.Contains(p.Id)).ToList();

            var result = details.GroupBy(p => p.GoodsCode).Select(p => new PurchaseGoodsResult
            {
                count = p.Count(),
                Amount = p.Sum(z => z.PurchaseMoney),
                GoodsCode = p.Key,
                GoodsBarCode = p.First().GoodsBarCode,
                OrderQty = p.Sum(z => z.PurchaseQty),
                PurchasePrice = p.First().PurchasePrice,
                GoodsName = goods.Where(z => z.Id == p.Key).First().GoodsName,
                SupName = manages.Where(z => z.Id == p.First().ManageId).First().SupCode
            });

            var sta = new
            {
                count = result.Count(),
                purchasecount = result.Sum(p => p.OrderQty),
                avgprice = result.Sum(p => p.OrderQty) == 0 ? 0 : result.Sum(p => p.PurchasePrice) / result.Sum(p => p.OrderQty),
                amount = result.Sum(p => p.Amount)
            };

            var returnValue = result.ToList().ToSearchResult(0);
            returnValue.Statistics = sta;

            return returnValue;
        }

        public SearchResult<PurchaseSupplierResult> SearchForPurchaseSupllier(DateTime? dateS, DateTime? dateE, string BranchId, int pageSize = 20, int pageIndex = 1)
        {
            var q = from l in EntityRepository.LinqQuery where l.IfExamine.ToLower() == "1" select l;
            if (dateS == null || dateE == null)
            {
                q = from l in q
                    where l.bCode == BranchId
                    select l;
            }
            if (dateS <= dateE && string.IsNullOrEmpty(BranchId) == false)
            {
                q = from l in q
                    where l.PurchaseDate >= dateS
                        && l.PurchaseDate <= dateE
                        && l.bCode == BranchId
                    select l;
            }
            //if (string.IsNullOrEmpty(SupCode))
            //{
            //    q = from l in q where l.SupCode == SupCode select l;
            //}
            var manages = q.OrderByDescending(p => p.Id).ToList();
            var manageIds = manages.Select(p => p.Id).ToArray();

            var details = (from l in PurchaseDetailRepository.LinqQuery
                           where
                               manageIds.Contains(l.ManageId)
                           select l).ToList();

            var suppliers = manages.GroupBy(p => p.EnCode).Select(p => new PurchaseSupplierResult
            { 
                SupName = p.First().SupCode,
                goodsCount = Convert.ToInt32(details.Where(z=>z.ManageId==p.First().Id).Sum(x=>x.PurchaseQty)),
                amount = Convert.ToInt32(details.Where(z => z.ManageId == p.First().Id).Sum(x => x.PurchaseMoney))
            });

            var sta = new { 
                count=suppliers.Count(),
                goodsCount = suppliers.Sum(p=>p.goodsCount),
                amount=suppliers.Sum(p=>p.amount)
            };

            var result = suppliers.ToList().ToSearchResult(0);
            result.Statistics = sta;
            return result;
        }

        public IList<PcPurchaseManage> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.SupCode.Contains(key)
                    || l.EnCode.Contains(key)
                    || l.PcForm.Contains(key)
                    || l.dCode.Contains(key)
                    || l.bCode.Contains(key)
                    || l.PcType.Contains(key)
                    || l.PcMode.Contains(key)
                    || l.WhCode.Contains(key)
                    || l.IfCheck.Contains(key)
                    || l.IfPutin.Contains(key)
                    || l.Operator.Contains(key)
                    || l.Assessor.Contains(key)
                    || l.IfExamine.Contains(key)
                    select l;


            }
            q = q.OrderByDescending(p => p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
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

        public bool IsGoodsHavePurchase(string goodCode)
        {
            return PurchaseDetailRepository.LinqQuery.Any(p => p.GoodsCode == goodCode);
        }

        public bool IsSupplierHavePurchase(string supCode)
        {
            return EntityRepository.LinqQuery.Any(p => p.EnCode == supCode);
        }

        public bool IsBranchHavePurchase(string bCode)
        {
            return EntityRepository.LinqQuery.Any(p => p.bCode == bCode);
        }
    }
}


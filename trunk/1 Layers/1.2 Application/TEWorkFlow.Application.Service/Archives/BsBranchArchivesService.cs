using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Transaction.Interceptor;
using TEWorkFlow.Domain.Archives;
using TEWorkFlow.Domain.Category;
using TEWorkFlow.Dto;
using TEWorkFlow.Application.Service.Sys;

namespace TEWorkFlow.Application.Service.Archives
{
    public class BsBranchArchivesService : IBsBranchArchivesService
    {

        public IRepositoryGUID<BsBranchArchives> EntityRepository { get; set; }
        public IRepositoryGUID<BsPaClass> BsPaClassRepository { get; set; }
        public IRepositoryGUID<BsPaArea> BsPaAreaRepository { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }
        public IRepositoryGUID<FbSupplierBranchRelation> SupplierBranchRepository { get; set; }
        [Transaction]
        public string Create(BsBranchArchives entity)
        {
            var id = EntityRepository.Save(entity);
            TfDataDownloadService.AddAllArchivesToBranch(id);
            return id;

        }

        [Transaction]
        public BsBranchArchives GetById(string id)
        {
            return EntityRepository.Get(id);
        }

        [Transaction]
        public IList<BsBranchArchives> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(BsBranchArchives entity)
        {
            bool Exist = true;
            if (EntityRepository.LinqQuery.Any(p => p.Id == entity.Id) == false)
            {
                Exist = false;

            }
            EntityRepository.SaveOrUpdate(entity);
            if (Exist == false)
            {
                TfDataDownloadService.AddAllArchivesToBranch(entity.Id);
            }
        }



        [Transaction]
        public void Delete(BsBranchArchives entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<BsBranchArchives> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }

        private void FillClassName(IList<BsBranchArchives> entities)
        {
            var classes = BsPaClassRepository.GetList();
            foreach (BsBranchArchives each in entities)
            {
                var q = classes.Where(p => p.Id == each.ClassCode);
                if (q.Count() > 0)
                {
                    each.ClassName = q.First().ClassName;
                }
            }
        }
        private void FillAreaName(IList<BsBranchArchives> entities)
        {
            var areas = BsPaAreaRepository.GetList();
            foreach (BsBranchArchives each in entities)
            {
                var q = areas.Where(p => p.Id == each.AreaCode);
                if (q.Count() > 0)
                {
                    each.AreaName = q.First().AreaName;
                }
            }
        }

        [Transaction]
        public SearchResult<BsBranchArchives> Search(SearchDtoBase<BsBranchArchives> c, string supCode = "")
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.bName) == false)
                {
                    q = q.Where(p => p.bName.Contains(c.entity.bName));
                }
                if (string.IsNullOrEmpty(c.entity.PyCode) == false)
                {
                    q = q.Where(p => p.PyCode.Contains(c.entity.PyCode));
                }
                if (string.IsNullOrEmpty(c.entity.bType) == false)
                {
                    q = q.Where(p => p.bType.Contains(c.entity.bType));
                }
                if (string.IsNullOrEmpty(c.entity.bState) == false)
                {
                    q = q.Where(p => p.bState.Contains(c.entity.bState));
                }
                if (string.IsNullOrEmpty(c.entity.AreaCode) == false)
                {
                    q = q.Where(p => p.AreaCode==c.entity.AreaCode);
                }
                if (string.IsNullOrEmpty(c.entity.ClassCode) == false)
                {
                    q = q.Where(p => p.ClassCode.Contains(c.entity.ClassCode));
                }
                if (string.IsNullOrEmpty(c.entity.Functionary) == false)
                {
                    q = q.Where(p => p.Functionary.Contains(c.entity.Functionary));
                }
                if (string.IsNullOrEmpty(c.entity.FunctionaryPhone) == false)
                {
                    q = q.Where(p => p.FunctionaryPhone.Contains(c.entity.FunctionaryPhone));
                }
                if (string.IsNullOrEmpty(c.entity.ContactAddress) == false)
                {
                    q = q.Where(p => p.ContactAddress.Contains(c.entity.ContactAddress));
                }
                if (string.IsNullOrEmpty(c.entity.OfficePhone) == false)
                {
                    q = q.Where(p => p.OfficePhone.Contains(c.entity.OfficePhone));
                }
                if (string.IsNullOrEmpty(c.entity.FaxPhone) == false)
                {
                    q = q.Where(p => p.FaxPhone.Contains(c.entity.FaxPhone));
                }
                if (string.IsNullOrEmpty(c.entity.eMail) == false)
                {
                    q = q.Where(p => p.eMail.Contains(c.entity.eMail));
                }
                if (string.IsNullOrEmpty(c.entity.Postalcode) == false)
                {
                    q = q.Where(p => p.Postalcode.Contains(c.entity.Postalcode));
                }
                if (string.IsNullOrEmpty(c.entity.OpenAccount) == false)
                {
                    q = q.Where(p => p.OpenAccount.Contains(c.entity.OpenAccount));
                }
                if (string.IsNullOrEmpty(c.entity.BankAccount) == false)
                {
                    q = q.Where(p => p.BankAccount.Contains(c.entity.BankAccount));
                }
                if (string.IsNullOrEmpty(c.entity.TaxNumber) == false)
                {
                    q = q.Where(p => p.TaxNumber.Contains(c.entity.TaxNumber));
                }
                if (string.IsNullOrEmpty(c.entity.OpCode) == false)
                {
                    q = q.Where(p => p.OpCode.Contains(c.entity.OpCode));
                }
                if (string.IsNullOrEmpty(c.entity.StockVoucher) == false)
                {
                    q = q.Where(p => p.StockVoucher.Contains(c.entity.StockVoucher));
                }
                if (c.entity.InputTax > 0)
                {
                    q = q.Where(p => p.InputTax == c.entity.InputTax);
                }

                if (string.IsNullOrEmpty(c.entity.BalanceMode) == false)
                {
                    q = q.Where(p => p.BalanceMode.Contains(c.entity.BalanceMode));
                }
                if (string.IsNullOrEmpty(c.entity.PayMode) == false)
                {
                    q = q.Where(p => p.PayMode.Contains(c.entity.PayMode));
                }
                if (string.IsNullOrEmpty(c.entity.BalancePeriod) == false)
                {
                    q = q.Where(p => p.BalancePeriod.Contains(c.entity.BalancePeriod));
                }
                if (c.entity.BalanceDay > 0)
                {
                    q = q.Where(p => p.BalanceDay == c.entity.BalanceDay);
                }

                if (c.entity.DeliveryDays > 0)
                {
                    q = q.Where(p => p.DeliveryDays == c.entity.DeliveryDays);
                }

                if (string.IsNullOrEmpty(c.entity.SupplyPriceType) == false)
                {
                    q = q.Where(p => p.SupplyPriceType.Contains(c.entity.SupplyPriceType));
                }
                if (c.entity.GrossRate > 0)
                {
                    q = q.Where(p => p.GrossRate == c.entity.GrossRate);
                }

                if (string.IsNullOrEmpty(c.entity.WhCode) == false)
                {
                    q = q.Where(p => p.WhCode.Contains(c.entity.WhCode));
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
            if (string.IsNullOrEmpty(c.key) == false)
            {
                q = q.Where(p => p.bName.Contains(c.key)
                    || p.PyCode.Contains(c.key)
                    || p.Id == c.key
                    || p.Functionary.Contains(c.key)
                    || p.FunctionaryPhone.Contains(c.key)
                    || p.OfficePhone.Contains(c.key));
            }

            if (string.IsNullOrEmpty(supCode) == false)
            {
                //var bcodes = SupplierBranchRepository.LinqQuery.Where(p => p.SupCode == supCode && p.Available == true).ToList().Select(p => p.bCode).ToArray();
                //q = q.Where(p => bcodes.Contains(p.Id));
                q = q.Where(p => p.IfExamine == "true" || p.IfExamine == "1");
            }
            int count = q.Count();
            q = q.OrderByDescending(p => p.Id).Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            FillClassName(result);
            FillAreaName(result);
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<BsBranchArchives> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.bName.Contains(key)
                    || l.PyCode.Contains(key)
                    || l.bType.Contains(key)
                    || l.bState.Contains(key)
                    || l.AreaCode==key
                    || l.ClassCode.Contains(key)
                    || l.Functionary.Contains(key)
                    || l.FunctionaryPhone.Contains(key)
                    || l.ContactAddress.Contains(key)
                    || l.OfficePhone.Contains(key)
                    || l.FaxPhone.Contains(key)
                    || l.eMail.Contains(key)
                    || l.Postalcode.Contains(key)
                    || l.OpenAccount.Contains(key)
                    || l.BankAccount.Contains(key)
                    || l.TaxNumber.Contains(key)
                    || l.OpCode.Contains(key)
                    || l.StockVoucher.Contains(key)
                    || l.BalanceMode.Contains(key)
                    || l.PayMode.Contains(key)
                    || l.BalancePeriod.Contains(key)
                    || l.SupplyPriceType.Contains(key)
                    || l.WhCode.Contains(key)
                    || l.Operator.Contains(key)
                    || l.Assessor.Contains(key)
                    || l.IfExamine.Contains(key)
                    select l;


            }
            q = q.OrderByDescending(p => p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillClassName(result);
            FillAreaName(result);
            return result.ToList();
        }

        [Transaction]
        public IList<BsBranchArchives> Search(string key, string supCode, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.bName.Contains(key)
                    || l.PyCode.Contains(key)
                    || l.bType.Contains(key)
                    || l.bState.Contains(key)
                    || l.AreaCode==key
                    || l.ClassCode.Contains(key)
                    || l.Functionary.Contains(key)
                    || l.FunctionaryPhone.Contains(key)
                    || l.ContactAddress.Contains(key)
                    || l.OfficePhone.Contains(key)
                    || l.FaxPhone.Contains(key)
                    || l.eMail.Contains(key)
                    || l.Postalcode.Contains(key)
                    || l.OpenAccount.Contains(key)
                    || l.BankAccount.Contains(key)
                    || l.TaxNumber.Contains(key)
                    || l.OpCode.Contains(key)
                    || l.StockVoucher.Contains(key)
                    || l.BalanceMode.Contains(key)
                    || l.PayMode.Contains(key)
                    || l.BalancePeriod.Contains(key)
                    || l.SupplyPriceType.Contains(key)
                    || l.WhCode.Contains(key)
                    || l.Operator.Contains(key)
                    || l.Assessor.Contains(key)
                    || l.IfExamine.Contains(key)
                    select l;


            }
            if (string.IsNullOrEmpty(supCode) == false)
            {
                //var bcodes = SupplierBranchRepository.LinqQuery.Where(p => p.SupCode == supCode && p.Available == true).ToList().Select(p => p.bCode).ToArray();
                //q = q.Where(p => bcodes.Contains(p.Id));
                q = q.Where(p => p.IfExamine == "true" || p.IfExamine == "1");
            }

            q = q.OrderByDescending(p => p.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillClassName(result);
            FillAreaName(result);
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
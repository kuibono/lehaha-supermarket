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
using TEWorkFlow.Domain.Sys;
using TEWorkFlow.Application.Service.Sys;
using NSH.VSTO;
namespace TEWorkFlow.Application.Service.Archives
{
    public class FbSupplierArchivesService : IFbSupplierArchivesService
    {

        public IRepositoryGUID<FbSupplierArchives> EntityRepository { get; set; }
        public IRepositoryGUID<FbPaSupType> PaSupTypeRepository { get; set; }
        public IRepositoryGUID<GoodsArchives> GoodsRepository { get; set; }
        //public IRepositoryGUID<TfDataDownload> DataDownloadRepository { get; set; }
        public ITfDataDownloadService TfDataDownloadService { get; set; }

        [Transaction]
        public string Create(FbSupplierArchives entity)
        {
            entity.LoginName = GenerateLoginName();
            entity.LoginPass = "111111";
            entity.Id = GenerateId();
            string id=EntityRepository.Save(entity);
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = id, DownloadTablename = "fb_supplier_archives" });
            if (entity.IfExamine == "1")
            {
                TfDataDownloadService.AddDownload("fb_supplier_archives", id);
            }
            Cache.Clear("_AllSuppliers");
            Cache.Clear("_AllSupplierNameValuePair");
            
            return id;
        }

        [Transaction]
        public FbSupplierArchives GetById(string id)
        {
            return EntityRepository.LinqQuery.AsCache("_AllSuppliers").Where(p => p.Id == id).FirstOrDefault();
        }

        [Transaction]
        public IList<FbSupplierArchives> GetAll()
        {
            var result = EntityRepository.LinqQuery.ToList();

            return result;
        }


        [Transaction]
        public void Update(FbSupplierArchives entity)
        {
            EntityRepository.Update(entity);
            if (entity.IfExamine == "1")
            {
                TfDataDownloadService.AddDownload("fb_supplier_archives", entity.Id);
            }
            Cache.Clear("_AllSuppliers");
            Cache.Clear("_AllSupplierNameValuePair");
            //DataDownloadRepository.Save(new TfDataDownload() { Id = Guid.NewGuid().ToString(), DownloadKeyvalue = entity.Id, DownloadTablename = "fb_supplier_archives" });
        }

        [Transaction]
        public void Delete(FbSupplierArchives entity)
        {
            EntityRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<FbSupplierArchives> entitys)
        {
            foreach (var entity in entitys)
            {
                EntityRepository.Delete(entity);
            }
        }


        [Transaction]
        public SearchResult<FbSupplierArchives> Search(SearchDtoBase<FbSupplierArchives> c)
        {
            var q = EntityRepository.LinqQuery;
            if (c.entity != null)
            {

                if (string.IsNullOrEmpty(c.entity.SupName) == false)
                {
                    q = q.Where(p => p.SupName.Contains(c.entity.SupName));
                }
                if (string.IsNullOrEmpty(c.entity.PyCode) == false)
                {
                    q = q.Where(p => p.PyCode.Contains(c.entity.PyCode));
                }
                if (string.IsNullOrEmpty(c.entity.SupTypeCode) == false)
                {
                    q = q.Where(p => p.SupTypeCode.Contains(c.entity.SupTypeCode));
                }
                if (string.IsNullOrEmpty(c.entity.Functionary) == false)
                {
                    q = q.Where(p => p.Functionary.Contains(c.entity.Functionary));
                }
                if (string.IsNullOrEmpty(c.entity.FunctionaryPhone) == false)
                {
                    q = q.Where(p => p.FunctionaryPhone.Contains(c.entity.FunctionaryPhone));
                }
                if (string.IsNullOrEmpty(c.entity.Linkman) == false)
                {
                    q = q.Where(p => p.Linkman.Contains(c.entity.Linkman));
                }
                if (string.IsNullOrEmpty(c.entity.ContactPhone) == false)
                {
                    q = q.Where(p => p.ContactPhone.Contains(c.entity.ContactPhone));
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

                if (string.IsNullOrEmpty(c.entity.OfferMode) == false)
                {
                    q = q.Where(p => p.OfferMode.Contains(c.entity.OfferMode));
                }
                if (c.entity.DeliveryDays > 0)
                {
                    q = q.Where(p => p.DeliveryDays == c.entity.DeliveryDays);
                }

                if (c.entity.PoolRate > 0)
                {
                    q = q.Where(p => p.PoolRate == c.entity.PoolRate);
                }

                if (c.entity.FloorsMoney > 0)
                {
                    q = q.Where(p => p.FloorsMoney == c.entity.FloorsMoney);
                }

                if (c.entity.ExcessRate > 0)
                {
                    q = q.Where(p => p.ExcessRate == c.entity.ExcessRate);
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
            FillFbPaSupType(result);
            return result.ToSearchResult(count);
        }

        [Transaction]
        public IList<FbSupplierArchives> Search(string key, int pageSize = 20, int pageIndex = 1)
        {
            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.SupName.Contains(key)
                    || l.PyCode.Contains(key)
                    select l;


            }
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            FillFbPaSupType(result);
            return result.ToList();
        }

        [Transaction]
        public IList<FbSupplierArchives> SearchWithGoodsCount(string key, int pageSize = 10000, int pageIndex = 1)
        {
            var goods = GoodsRepository.LinqQuery.ToList();

            var q = EntityRepository.LinqQuery;
            if (string.IsNullOrEmpty(key) == false)
            {
                q = from l in q
                    where
                    l.Id.Contains(key)
                    || l.SupName.Contains(key)
                    || l.PyCode.Contains(key)
                    select l;


            }
            q = q.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            var result = q.ToList();
            result = result.Select(p => {
                var item = p; 
                if (p.SupName.EndsWith(")") == false)
                {
                    //p.SupName = p.SupName + "(" + goods.Count(x => x.SupCode == p.Id) + ")";
                    p.GoodsCount = goods.Count(x => x.SupCode == p.Id);
                }
                return item;
            }).ToList();
            FillFbPaSupType(result);
            return result.ToList();
        }

        private void FillFbPaSupType(IList<FbSupplierArchives> entities)
        {
            var type = PaSupTypeRepository.GetList();
            foreach (FbSupplierArchives fbSupplierArchivese in entities)
            {
                var q = type.Where(p => p.Id == fbSupplierArchivese.SupTypeCode);
                if (q.Count() > 0)
                {
                    fbSupplierArchivese.SupTypeName = q.First().SupTypeName;
                }
            }
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

        [Transaction]
        public string GenerateLoginName()
        {
            string result = GenerateString();
            while(EntityRepository.LinqQuery.Any(p=>p.LoginName==result))
            {
                result = GenerateString();
            }
            return result;
        }
        [Transaction]
        public string GenerateId()
        {
            string result = Genarate4Id();
            while (EntityRepository.LinqQuery.Any(p => p.Id == result))
            {
                result = Genarate4Id();
            }
            return result;
        }

        private string GenerateString()
        {
            Guid randSeedGuid = Guid.NewGuid();

            Random random = new Random(BitConverter.ToInt32(randSeedGuid.ToByteArray(), 0));
            return  random.Next(10001, 99999).ToString();
        }

        private string Genarate4Id()
        {
            Guid randSeedGuid = Guid.NewGuid();

            Random random = new Random(BitConverter.ToInt32(randSeedGuid.ToByteArray(), 0));
            return random.Next(1001, 9999).ToString();
        }
    }
}

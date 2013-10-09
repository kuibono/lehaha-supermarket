using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NSH.Core.Domain.Specifications;
using TEWorkFlow.Domain.Sys;
//using TEWorkFlow.Domain.WorkFlow;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using NSH.Core.DataPage;
//using NSH.Authorization.Domain;
using TEWorkFlow.Dto;
using TEWorkFlow.Domain.Archives;

namespace TEWorkFlow.Application.Service.Sys
{
    public class SysLoginPowerService : ISysLoginPowerService
    {
        public IRepositoryGUID<SysLoginPower> SysLoginPowerRepository { get; set; }
        public IRepositoryGUID<Ememployeearchive> EmployeeRepository { get; set; }
        public IRepositoryGUID<FbSupplierArchives> SupplierRepository { get; set; }
        [Transaction]
        public string Create(SysLoginPower entity)
        {
            return SysLoginPowerRepository.Save(entity);
        }

        [Transaction]
        public SysLoginPower GetById(string id)
        {
            return SysLoginPowerRepository.Get(id);
        }

        [Transaction]
        public IList<SysLoginPower> GetAll()
        {
            return SysLoginPowerRepository.LinqQuery.ToList();
        }

        [Transaction]
        public void Update(SysLoginPower entity)
        {
            SysLoginPowerRepository.Update(entity);
        }

        [Transaction]
        public void Delete(SysLoginPower entity)
        {
            SysLoginPowerRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<SysLoginPower> entitys)
        {
            foreach (var entity in entitys)
            {
                SysLoginPowerRepository.Delete(entity);
            }
        }

        [Transaction]
        public SysLoginPower CheckUser(string UserName, string Password)
        {
            var q = SysLoginPowerRepository.LinqQuery.Where(p => p.Username == UserName && p.Userpw == Password);
            if (q.Count() > 0)
            {
                return q.First();
            }
            else
            {
                return null;
            }
        }

        [Transaction]
        public LoginResult CheckUser(string UserName, string Password, int UserType)
        {
            LoginResult result = new LoginResult();
            if (UserType == 0)
            {
                //员工
                var q = from l in EmployeeRepository.LinqQuery
                        where l.LoginName == UserName
                            && l.LoginPass == Password
                        select l;
                if (q.Count() > 0)
                {
                    result.Employee = q.First();
                    result.IsSuccess = true;
                }
                else
                {
                    result.Text = "账号或密码错误，请重试";
                    result.IsSuccess = false;
                }
            }
            else
            {
                //供货商
                var q = from l in SupplierRepository.LinqQuery
                        where l.LoginName == UserName
                            && l.LoginPass == Password
                        select l;
                if (q.Count() > 0)
                {
                    result.Supplier = q.First();
                    if (result.Supplier.IfExamine != "1" && result.Supplier.IfExamine != "true")
                    {
                        result.Supplier = null;
                        result.IsSuccess = false;
                        result.Text = "账号已禁用或未审核";

                    }
                    else
                    {
                        result.IsSuccess = true;
                    }
                }
                else
                {
                    result.Text = "账号或密码错误，请重试";
                    result.IsSuccess = false;
                }

            }

            return result;
        }

        [Transaction]
        public IList<SysLoginPower> GetList(ISpecification<SysLoginPower> specification, Action<Orderable<SysLoginPower>> order)
        {
            return SysLoginPowerRepository.GetList(specification, order);
        }

        [Transaction]
        public SearchResult<SysLoginPower> Search(SearchDtoBase<SysLoginPower> c)
        {
            var q = SysLoginPowerRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.Username) == false)
                {
                    q = q.Where(p => p.Username.Contains(c.entity.Username));
                }
                if (string.IsNullOrEmpty(c.entity.Emname) == false)
                {
                    q = q.Where(p => p.Emname.Contains(c.entity.Emname));
                }
                if (c.entity.UserType > 0)
                {
                    q = q.Where(p => p.UserType == c.entity.UserType);
                }
                if (string.IsNullOrEmpty(c.entity.Userstate) == false)
                {
                    q = q.Where(p => p.Userstate.Contains(c.entity.Userstate));
                }
            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            return q.ToList().ToSearchResult(count);
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = SysLoginPowerRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (SysLoginPower sysLoginPower in q)
            {
                Delete(sysLoginPower);
            }
        }
          
    }
}

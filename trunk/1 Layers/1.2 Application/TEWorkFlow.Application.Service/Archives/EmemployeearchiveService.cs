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

namespace TEWorkFlow.Application.Service.Archives
{
    public class EmemployeearchiveService:IEmemployeearchiveService
    {

        public IRepositoryGUID<Ememployeearchive> EmemployeearchiveRepository { get; set; }
        public IRepositoryGUID<EmPaPolitics> EmPaPoliticsRepository { get; set; }

        [Transaction]
        public string Create(Ememployeearchive entity)
        {
            return EmemployeearchiveRepository.Save(entity);
        }

        [Transaction]
        public Ememployeearchive GetById(string id)
        {
            return EmemployeearchiveRepository.Get(id);
        }

        [Transaction]
        public IList<Ememployeearchive> GetAll()
        {
            var result=EmemployeearchiveRepository.LinqQuery.ToList();
           
            return result;
        }

        private void FillPoliticName(IList<Ememployeearchive> result)
        {
            var politics = EmPaPoliticsRepository.LinqQuery.ToList();
            foreach (Ememployeearchive ememployeearchive in result)
            {
                var q = politics.Where(p => p.Id == ememployeearchive.Politicscode);
                if (q.Count() > 0)
                {
                    ememployeearchive.PoliticsName = q.First().Name;
                }
            }
        }

        [Transaction]
        public void Update(Ememployeearchive entity)
        {
            EmemployeearchiveRepository.Update(entity);
        }

        [Transaction]
        public void Delete(Ememployeearchive entity)
        {
            EmemployeearchiveRepository.Delete(entity);
        }

        [Transaction]
        public void Delete(IEnumerable<Ememployeearchive> entitys)
        {
            foreach (var entity in entitys)
            {
                EmemployeearchiveRepository.Delete(entity);
            }
        }


        [Transaction]
        public IList<Ememployeearchive> GetList(ISpecification<Ememployeearchive> specification, Action<Orderable<Ememployeearchive>> order)
        {
            return EmemployeearchiveRepository.GetList(specification, order);
        }

        [Transaction]
        public SearchResult<Ememployeearchive> Search(SearchDtoBase<Ememployeearchive> c)
        {
            var q = EmemployeearchiveRepository.LinqQuery;
            if (c.entity != null)
            {
                if (string.IsNullOrEmpty(c.entity.Emname) == false)
                {
                    q = q.Where(p => p.Emname.Contains(c.entity.Emname));
                }
                if (string.IsNullOrEmpty(c.entity.Nameused) == false)
                {
                    q = q.Where(p => p.Nameused.Contains(c.entity.Nameused));
                }
                //Birthday
                if (string.IsNullOrEmpty(c.entity.Sex) == false)
                {
                    q = q.Where(p => p.Sex.Contains(c.entity.Sex));
                }
                if (string.IsNullOrEmpty(c.entity.Politicscode) == false)
                {
                    q = q.Where(p => p.Politicscode.Contains(c.entity.Politicscode));
                }
                if (string.IsNullOrEmpty(c.entity.Nationcode) == false)
                {
                    q = q.Where(p => p.Nationcode.Contains(c.entity.Nationcode));
                }
                if (string.IsNullOrEmpty(c.entity.Educationcode) == false)
                {
                    q = q.Where(p => p.Educationcode.Contains(c.entity.Educationcode));
                }
                if (string.IsNullOrEmpty(c.entity.Professionaltitlecode) == false)
                {
                    q = q.Where(p => p.Professionaltitlecode.Contains(c.entity.Professionaltitlecode));
                }
                if (string.IsNullOrEmpty(c.entity.Ifmarried) == false)
                {
                    q = q.Where(p => p.Ifmarried.Contains(c.entity.Ifmarried));
                }
                if (string.IsNullOrEmpty(c.entity.Nativeplace) == false)
                {
                    q = q.Where(p => p.Nativeplace.Contains(c.entity.Nativeplace));
                }
                if (string.IsNullOrEmpty(c.entity.Idcard) == false)
                {
                    q = q.Where(p => p.Idcard.Contains(c.entity.Idcard));
                }
                //Accededay

                if (string.IsNullOrEmpty(c.entity.Familyphone) == false)
                {
                    q = q.Where(p => p.Familyphone.Contains(c.entity.Familyphone));
                }
                if (string.IsNullOrEmpty(c.entity.Handset) == false)
                {
                    q = q.Where(p => p.Handset.Contains(c.entity.Handset));
                }
                if (string.IsNullOrEmpty(c.entity.Email) == false)
                {
                    q = q.Where(p => p.Email.Contains(c.entity.Email));
                }
                if (string.IsNullOrEmpty(c.entity.Familyaddress) == false)
                {
                    q = q.Where(p => p.Familyaddress.Contains(c.entity.Familyaddress));
                }
                if (string.IsNullOrEmpty(c.entity.Postalcode) == false)
                {
                    q = q.Where(p => p.Postalcode.Contains(c.entity.Postalcode));
                }
                if (string.IsNullOrEmpty(c.entity.Graduateacademy) == false)
                {
                    q = q.Where(p => p.Graduateacademy.Contains(c.entity.Graduateacademy));
                }
                //Graduateday

                if (string.IsNullOrEmpty(c.entity.Dcode) == false)
                {
                    q = q.Where(p => p.Dcode.Contains(c.entity.Dcode));
                }
                if (string.IsNullOrEmpty(c.entity.Bcode) == false)
                {
                    q = q.Where(p => p.Bcode.Contains(c.entity.Bcode));
                }
                if (string.IsNullOrEmpty(c.entity.Dutycode) == false)
                {
                    q = q.Where(p => p.Dutycode.Contains(c.entity.Dutycode));
                }
                if (string.IsNullOrEmpty(c.entity.Bankaccount) == false)
                {
                    q = q.Where(p => p.Bankaccount.Contains(c.entity.Bankaccount));
                }
                if (string.IsNullOrEmpty(c.entity.Pensionaccount) == false)
                {
                    q = q.Where(p => p.Pensionaccount.Contains(c.entity.Pensionaccount));
                }
                if (string.IsNullOrEmpty(c.entity.Emstate) == false)
                {
                    q = q.Where(p => p.Emstate.Contains(c.entity.Emstate));
                }
                if (c.entity.Stature>0)
                {
                    q = q.Where(p => p.Stature == c.entity.Stature);
                }
                if (c.entity.Width > 0)
                {
                    q = q.Where(p => p.Width == c.entity.Width);
                }
            }
            int count = q.Count();

            q = q.Skip((c.pageIndex) * c.pageSize).Take(c.pageSize);
            var result = q.ToList();
            FillPoliticName(result);
            return result.ToSearchResult(count);
        }

        [Transaction]
        public void Delete(IList<string> ids)
        {
            var q = EmemployeearchiveRepository.LinqQuery.Where(p => ids.Contains(p.Id));
            foreach (Ememployeearchive Ememployeearchive in q)
            {
                Delete(Ememployeearchive);
            }
        }

        [Transaction]
        public string GenerateId()
        {
            string result = GenerateString();
            while (EmemployeearchiveRepository.LinqQuery.Any(p => p.Id == result))
            {
                result = GenerateString();
            }
            return result;
        }

        private string GenerateString()
        {
            Guid randSeedGuid = Guid.NewGuid();

            Random random = new Random(BitConverter.ToInt32(randSeedGuid.ToByteArray(), 0));
            return random.Next(10001, 99999).ToString();
        }
    }
}

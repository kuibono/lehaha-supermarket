using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.NHibernate.Generic.Support;
using NHibernate.Linq;
using NSH.Core.Domain;
using System.Linq.Expressions;
using NSH.Core.Domain.Specifications;


namespace NSH.Repository
{
    public class Repository<TEntity> : HibernateDaoSupport, IRepository<TEntity> where TEntity : IAggregateRoot
    {
        public virtual IQueryable<TEntity> LinqQuery
        {
            get { return new NhQueryable<TEntity>(Session.GetSessionImplementation()); }
            
        }

        public virtual NHibernate.ISession Session
        {
            get
            {
                return Session;
            }
        }

        public virtual TEntity Get(int id)
        {
            return HibernateTemplate.Get<TEntity>(id);
        }

        public virtual int Save(TEntity entity)
        {
            int r;
            object o = HibernateTemplate.Save(entity);
            int.TryParse(o.ToString(), out r);
            return r;
        }

        public virtual void Update(TEntity entity)
        {
            HibernateTemplate.Update(entity);
        }

        public virtual void SaveOrUpdate(TEntity entity)
        {
            HibernateTemplate.SaveOrUpdate(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            HibernateTemplate.Delete(entity);
        }

        public void Clear()
        {
            HibernateTemplate.Clear();
        }

        public void Refresh(TEntity entity)
        {
            HibernateTemplate.Refresh(entity);
        }

        public virtual void Flush()
        {
            HibernateTemplate.Flush();
        }

        public virtual IList<TEntity> GetList()
        {
            return LinqQuery.ToList();
        }


        public IList<TEntity> GetList(ISpecification<TEntity> specification)
        {
            return LinqQuery.Where(specification.GetExpression()).ToList();
        }

        public IList<TEntity> GetList(ISpecification<TEntity> specification, Action<Orderable<TEntity>> order)
        {
            var orderable = new Orderable<TEntity>(LinqQuery.Where(specification.GetExpression()));
            order(orderable);
            return orderable.Queryable.ToList();
        }

        public IList<TEntity> GetList( Action<Orderable<TEntity>> order)
        {
            var orderable = new Orderable<TEntity>(LinqQuery);
            order(orderable);
            return orderable.Queryable.ToList();
        }

    }
}

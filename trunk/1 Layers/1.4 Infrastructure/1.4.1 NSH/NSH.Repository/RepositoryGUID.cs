﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using NSH.Core.Domain;
using NSH.Core.Domain.Specifications;
using Spring.Data.NHibernate.Generic.Support;
using Spring.Transaction.Interceptor;
using NSH.Core.Domain;
using NSH.Core.DataPage;
namespace NSH.Repository
{
    public class RepositoryGUID<TEntity> : HibernateDaoSupport, IRepositoryGUID<TEntity> where TEntity : IAggregateRootGUID
    {
        public virtual IQueryable<TEntity> LinqQuery
        {
            get { return new NhQueryable<TEntity>(Session.GetSessionImplementation()); }
        }

        public virtual TEntity Get(string id)
        {
            return HibernateTemplate.Get<TEntity>(id);
        }

        public virtual string Save(TEntity entity)
        {
            return HibernateTemplate.Save(entity).ToString();
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

        public IList<TEntity> GetList(Action<Orderable<TEntity>> order)
        {
            var orderable = new Orderable<TEntity>(LinqQuery);
            order(orderable);
            return orderable.Queryable.ToList();
        }
    }
}
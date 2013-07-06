using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using NSH.Core.Domain.Specifications;
using NHibernate;
namespace NSH.Core.Domain
{
    public interface IRepositoryGUID<TEntity> where TEntity : IAggregateRootGUID
    {
        /// <summary>
        /// Provides the main entry point to a LINQ query.
        /// </summary>
        IQueryable<TEntity> LinqQuery { get; }

        NHibernate.ISession Session { get; }

        /// <summary>
        /// Return the persistent instance of the given entity type with the given identifier,
        /// or null if not found.  Obtains the specified lock mode if the instance exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(string id);

        /// <summary>
        /// Persist the given transient instance with the given identifier.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string Save(TEntity entity);

        /// <summary>
        /// Update the given persistent instance.
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Save or update the given persistent instance, according to its id (matching
        /// the configured "unsaved-value"?).
        /// </summary>
        /// <param name="entity"></param>
        void SaveOrUpdate(TEntity entity);

        /// <summary>
        /// Delete the given persistent instance.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);

        /// <summary>
        /// Remove all objects from the Session cache, and cancel all pending saves,
        /// updates and deletes.
        /// </summary>
        void Clear();

        /// <summary>
        /// Re-read the state of the given persistent instance.
        /// </summary>
        /// <param name="entity"></param>
        void Refresh(TEntity entity);

        /// <summary>
        /// Flush all pending saves, updates and deletes to the database.
        /// </summary>
        void Flush();

        IList<TEntity> GetList();

        IList<TEntity> GetList(ISpecification<TEntity> specification);

        IList<TEntity> GetList(Action<Orderable<TEntity>> order);

        IList<TEntity> GetList(ISpecification<TEntity> specification, Action<Orderable<TEntity>> order);

        IList<TEntity> Exec(string sql);

        ISession GetSession();
    }
}

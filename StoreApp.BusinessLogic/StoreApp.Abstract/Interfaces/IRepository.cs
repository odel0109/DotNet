using StoreApp.Abstract.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace StoreApp.Abstract.Interfaces
{
    /// <summary>
    /// Unparametrized repository
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Save all changes to repository
        /// </summary>
        void CommitChanges();
    }

    /// <summary>
    /// Repository which is not support any updates
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public interface IReadonlyRepository<TEntityType>
        where TEntityType : IEntity
    {
        /// <summary>
        /// Returns <typeparamref name="TEntityType"/>s satisfied to criterias
        /// </summary>
        /// <param name="criteria">Can be null</param>
        /// <returns></returns>
        IQueryable<TEntityType> Read(QueryParameterSet<TEntityType> queryParameters);

        /// <summary>
        /// Returns all objects from repository
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntityType> ReadAll();

        /// <summary>
        /// Returns <typeparamref name="TEntityType"/> using searching by primary key
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        TEntityType Find(params object[] primaryKey);
    }

    /// <summary>
    /// Repository behaviour
    /// </summary>
    /// <typeparam name="TEntityType"></typeparam>
    public interface IRepository<TEntityType> : IReadonlyRepository<TEntityType>, IRepository
        where TEntityType : IEntity
    {

        /// <summary>
        /// Adds <typeparamref name="TEntityType"/> to repository
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntityType entity);

        /// <summary>
        /// Updates existing <typeparamref name="TEntityType"/> to repository
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newEntity"></param>
        void Update(TEntityType entity, TEntityType newEntity);

        /// <summary>
        /// Deletes <typeparamref name="TEntityType"/> from repository
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntityType entity);
    }
}

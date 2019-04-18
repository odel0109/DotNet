using StoreApp.Abstract.Extensions;
using StoreApp.Abstract.Interfaces;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace StoreApp.Abstract.EF
{
    /// <summary>
    /// Abstract entity repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EFAbstractDataContext<TEntity> : DbContext, IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public EFAbstractDataContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<EFAbstractDataContext<TEntity>>(null);
        }

        public EFAbstractDataContext() : this("StoreAppB")
        { }

        #region IRepository<T> implemetation

        public virtual void Add(TEntity entity)
        {
            Add<TEntity>(entity);
        }

        public virtual void CommitChanges()
        {
            SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            Delete<TEntity>(entity);
        }

        public virtual TEntity Find(params object[] primaryKey)
        {
            return Find<TEntity>(primaryKey);
        }

        public virtual IQueryable<TEntity> Read(QueryParameterSet<TEntity> queryParameters)
        {
            return Read<TEntity>(queryParameters);
        }

        public IQueryable<TEntity> ReadAll()
        {
            return Read(null);
        }

        public virtual void Update(TEntity entity, TEntity newEntity)
        {
            Update<TEntity>(entity, newEntity);
        }

        #endregion

        #region Generic methods

        public void Add<TRelatedEntity>(TRelatedEntity entity)
            where TRelatedEntity : class
        {
            Set<TRelatedEntity>().Add(entity);
        }

        public void Delete<TRelatedEntity>(TRelatedEntity entity)
            where TRelatedEntity : class
        {
            Set<TRelatedEntity>().Remove(entity);
        }
        public virtual TRelatedEntity Find<TRelatedEntity>(params object[] primaryKey)
            where TRelatedEntity : class
        {
            return Set<TRelatedEntity>().Find(primaryKey);
        }

        public virtual IQueryable<TRelateEntity> Read<TRelateEntity>(QueryParameterSet<TRelateEntity> queryParameters)
             where TRelateEntity : class
        {
            return Set<TRelateEntity>().ApplyCriteria(queryParameters);
        }

        public void Update<TRelateEntity>(TRelateEntity entity, TRelateEntity newEntity)
             where TRelateEntity : class
        {
            Entry(entity).CurrentValues.SetValues(newEntity);
        }

        #endregion

        /// <summary>
        /// Calls ConfigureType and ignores PrimaryKey property
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            ConfigureMainType(modelBuilder);

            modelBuilder.Entity<TEntity>().Ignore(t => t.PrimaryKey);

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Configuration of type
        /// </summary>
        /// <typeparam name="TEntityType"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <returns></returns>
        protected abstract EntityTypeConfiguration<TEntity> ConfigureMainType(DbModelBuilder modelBuilder);
    }
}

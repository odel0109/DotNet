using StoreApp.Abstract.EF;
using StoreApp.Abstract.Extensions;
using StoreApp.Abstract.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace StoreApp.EventData.EF
{
    public class EFDiscountDataContext : EFAbstractDataContext<Discount>, IRepository<Event>
    {
        public EFDiscountDataContext() : base()
        { }

        public EFDiscountDataContext(string connectionString) : base(connectionString)
        { }

        #region IRepository<Event> implementation
        public void Add(Event entity)
        {
            Add<Event>(entity);
        }

        public void Delete(Event entity)
        {
            Delete<Event>(entity);
        }

        public IQueryable<Event> Read(QueryParameterSet<Event> queryParameters)
        {
            return Read<Event>(queryParameters);
        }

        public void Update(Event entity, Event newEntity)
        {
            Update<Event>(entity, newEntity);
        }

        Event IReadonlyRepository<Event>.Find(params object[] primaryKey)
        {
            return Find<Event>(primaryKey);
        }

        IQueryable<Event> IReadonlyRepository<Event>.ReadAll()
        {
            return Read<Event>(null);
        }

        #endregion

        protected override EntityTypeConfiguration<Discount> ConfigureMainType(DbModelBuilder modelBuilder)
        {
            ConfigureEventType(modelBuilder);

            return ConfigureDiscountType(modelBuilder);
        }

        protected virtual EntityTypeConfiguration<Event> ConfigureEventType(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Event>();

            entity.ToTable("Event");
            entity.HasKey(p => new { p.EventID });

            entity.HasMany(e => e.Discounts)
                .WithRequired(d => d.Event);

            entity.Property(e => e.EventID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            entity.Property(e => e.EndTime).IsRequired();

            entity.Property(e => e.Name).IsRequired().IsUnicode().HasMaxLength(256);

            entity.Property(e => e.StartTime).IsRequired();

            return entity;
        }

        protected virtual EntityTypeConfiguration<Discount> ConfigureDiscountType(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<Discount>();

            entity.ToTable("Discount");
            entity.HasKey(p => new { p.DiscountID });

            entity.HasRequired(d => d.Event);

            entity.Property(d => d.DiscountID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).IsRequired();

            entity.Property(d => d.DiscountType).IsRequired();

            entity.Property(d => d.MinQuantity).IsOptional();

            entity.Property(d => d.Target).IsRequired();

            entity.Property(d => d.TargetType).IsRequired();

            entity.Property(d => d.Value).IsRequired();

            return entity;
        }
    }
}

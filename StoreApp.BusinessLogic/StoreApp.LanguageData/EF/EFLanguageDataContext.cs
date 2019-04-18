using StoreApp.Abstract.EF;
using StoreApp.LanguageData.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace StoreApp.LanguageData.EF
{
    public class EFLanguageDataContext : EFAbstractDataContext<MessageDetail>, ILanguageRepository<MessageDetail>
    {
        public EFLanguageDataContext() : base()
        { }

        public EFLanguageDataContext(string connectionString) : base(connectionString)
        { }

        /// <summary>
        /// TODO: Get values from database
        /// </summary>
        public IEnumerable<short> SupportedLanguages => new List<short> { 1033, 1035 };


        protected override EntityTypeConfiguration<MessageDetail> ConfigureMainType(DbModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<MessageDetail>();

            entity.ToTable("MessageDetail");
            entity.HasKey(d => new { d.MessageID, d.LanguageCode });

            entity.Property(l => l.MessageID).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            entity.Property(l => l.LanguageCode).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            entity.Property(l => l.Message).IsUnicode(true).HasMaxLength(2048);

            return entity;
        }
    }
}

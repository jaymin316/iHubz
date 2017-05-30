using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using iHubz.Domain.MainModule.ReferenceData;

namespace iHubz.Infrastructure.Data.MainModule.UnitOfWork.Mapping
{
    class StatesMap : EntityTypeConfiguration<States>
    {
        public StatesMap()
        {
            // Primary Key
            this.HasKey(t => t.StateId);

            // Properties
            this.Property(t => t.StateId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.StateName)
               .HasMaxLength(100);

            HasMany(t => t.Companies).WithRequired(t => t.State);

            // Table & Column Mappings
            this.ToTable("tState");
            this.Property(t => t.StateId).HasColumnName("StateID");
            this.Property(t => t.StateName).HasColumnName("StateName");
        }
    }
}

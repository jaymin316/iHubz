using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Infrastructure.Data.MainModule.UnitOfWork.Mapping
{
    class CompanyPropertiesMap : EntityTypeConfiguration<CompanyProperties>
    {
        public CompanyPropertiesMap()
        {
            // Primary Key
            HasKey(t => t.CompanyPropertyId);

            // Properties
            Property(t => t.CompanyPropertyId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CompanyId)
                .IsRequired();

            Property(t => t.BusinessCategory)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory1)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory2)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory3)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory4)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory5)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory6)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory7)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory8)
               .HasMaxLength(100);

            Property(t => t.BusinessCategory9)
               .HasMaxLength(100);

            Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            HasRequired(t => t.Company)
              .WithMany()
              .HasForeignKey(d => d.CompanyId);

            // Table & Column Mappings
            ToTable("tbl_CompanyProperties");
            Property(t => t.CompanyPropertyId).HasColumnName("CompanyPropertyID");
            Property(t => t.CompanyId).HasColumnName("CompanyID");
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using iHubz.Domain.MainModule.CompanyEntities;

namespace iHubz.Infrastructure.Data.MainModule.UnitOfWork.Mapping
{
    class CompaniesMap : EntityTypeConfiguration<Companies>
    {
        public CompaniesMap()
        {
            // Primary Key
            HasKey(t => t.CompanyId);

            // Properties
            Property(t => t.CompanyId)
                .IsRequired()
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            Property(t => t.ContactName1)
                .HasMaxLength(100);
            
            Property(t => t.ContactName2)
                .HasMaxLength(100);

            Property(t => t.AddressLine1)
                .HasMaxLength(500);

            Property(t => t.AddressLine2)
                .HasMaxLength(500);

            Property(t => t.City)
                .HasMaxLength(100);

            Property(t => t.District)
                .HasMaxLength(100);

            Property(t => t.StateId)
                .IsRequired();

            Property(t => t.Pincode)
                .HasMaxLength(6)
                .IsRequired();

            Property(t => t.Website)
                .HasMaxLength(100);

            Property(t => t.Email1)
                .HasMaxLength(100);

            Property(t => t.Email2)
                .HasMaxLength(100);

            Property(t => t.WorkPhone1)
                .HasMaxLength(50);

            Property(t => t.WorkPhone2)
                .HasMaxLength(50);

            Property(t => t.Mobile)
                .HasMaxLength(20);

            Property(t => t.CreatedBy)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.ModifiedBy)
                .IsRequired()
                .HasMaxLength(50);

            // Relationships
            HasMany(t => t.CompanyProperties).WithRequired(t => t.Company);

            // Table & Column Mappings
            ToTable("tbl_Company");
            Property(t => t.CompanyId).HasColumnName("CompanyID");
            Property(t => t.StateId).HasColumnName("StateID");
            
        }
    }
}

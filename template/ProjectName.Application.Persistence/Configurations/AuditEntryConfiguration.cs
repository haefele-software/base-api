using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectName.Application.Domain.Entities;

namespace ProjectName.Application.Persistence.Configurations
{
    public class AuditEntryConfiguration : IEntityTypeConfiguration<AuditEntry>
    {
        public void Configure(EntityTypeBuilder<AuditEntry> builder)
        {
            builder.Property(p => p.Id).HasColumnName("AuditEntryId").UseIdentityColumn();
            builder.OwnsOne(a => a.ReferenceId).Property(p => p.Value).HasColumnName("ReferenceId").HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.OwnsOne(a => a.AuditDate).Property(p => p.Value).HasColumnName("AuditDate").HasColumnType("datetimeoffset").IsRequired();
            builder.OwnsOne(a => a.AuditedEntityId).Property(p => p.Value).HasColumnName("AuditedEntityId").HasColumnType("BIGINT");
            builder.OwnsOne(a => a.AuditedEntityType).Property(p => p.Value).HasColumnName("AuditedEntityType").HasColumnType("varchar(512)");
            builder.OwnsOne(a => a.InitiatedBy).Property(p => p.Value).HasColumnName("InitiatedBy").HasColumnType("varchar(256)");
            builder.OwnsOne(a => a.ObjectNewState).Property(p => p.Value).HasColumnName("NewState").HasColumnType("NVARCHAR(MAX)");
            builder.OwnsOne(a => a.ObjectPreviousState).Property(p => p.Value).HasColumnName("PreviousState").HasColumnType("NVARCHAR(MAX)");

            builder.Property(a => a.State).HasColumnName("DataState").HasColumnType("TINYINT");
            builder.Property(p => p.AuditEvent).HasColumnName("AuditEvent").HasColumnType("TINYINT");
        }
    }
}

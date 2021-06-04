using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectName.Application.Domain.Entities;

namespace ProjectName.Application.Persistence.Configurations
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Ignore(e => e.Done);

            builder.Property(a => a.Id).HasColumnName("TodoItemId").UseIdentityColumn();
            builder.OwnsOne(a => a.ReferenceId).Property(p => p.Value).HasColumnName("ReferenceId").HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.OwnsOne(a => a.Title).Property(p => p.Value).HasColumnName("Title").HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.OwnsOne(a => a.CreatedDate).Property(p => p.Value).HasColumnName("CreateddDate").HasColumnType("DATETIMEOFFSET").IsRequired();
            builder.OwnsOne(a => a.ReminderDate).Property(p => p.Value).HasColumnName("ReminderDate").HasColumnType("DATETIMEOFFSET");
            builder.OwnsOne(a => a.Note).Property(p => p.Value).HasColumnName("Note").HasColumnType("NVARCHAR(MAX)");

            builder.Property(a => a.State).HasColumnType("TINYINT");
        }
    }
}

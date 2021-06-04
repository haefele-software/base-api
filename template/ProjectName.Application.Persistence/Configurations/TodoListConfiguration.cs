using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectName.Application.Domain.Entities;

namespace ProjectName.Application.Persistence.Configurations
{
    public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
    {
        public void Configure(EntityTypeBuilder<TodoList> builder)
        {
            builder.Property(a => a.Id).HasColumnName("TodoListId").UseIdentityColumn();
            builder.OwnsOne(a => a.ReferenceId).Property(p => p.Value).HasColumnName("ReferenceId").HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.OwnsOne(a => a.Title).Property(p => p.Value).HasColumnName("Title").HasColumnType("NVARCHAR(MAX)").IsRequired();
            builder.HasMany(a => a.Items).WithOne(m => m.TodoList).HasForeignKey("TodoListId");
            builder.Property(a => a.State).HasColumnType("TINYINT");
        }
    }
}

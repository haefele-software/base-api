using Microsoft.EntityFrameworkCore;
using ProjectName.Application.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectName.Application.Contracts.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<TodoList> TodoLists { get; set; }

        DbSet<AuditEntry> Audits { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

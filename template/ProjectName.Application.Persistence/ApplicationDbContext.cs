using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clean.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using ProjectName.Application.Contracts.Interfaces;
using ProjectName.Application.Domain.Common;
using ProjectName.Application.Domain.Entities;
using ProjectName.Application.Domain.Enums;
using ProjectName.Application.Domain.Events.AuditEntry;
using ProjectName.Application.Persistence.Extensions;

namespace ProjectName.Application.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
        }

        public DbSet<TodoItem> TodoItems { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        public DbSet<AuditEntry> Audits { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().Where(entry => entry.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is IHasDomainEvent @createdEntity)
                        {
                            @createdEntity.DomainEvents.Add(new AuditEntityEvent(entry.Entity, (Entity)entry.CurrentValues.ToObject(), AuditEvent.EntityAdded));
                        }

                        break;

                    case EntityState.Modified:
                        if (entry.Entity is IHasDomainEvent @modifiedEntity)
                        {
                            @modifiedEntity.DomainEvents.Add(new AuditEntityEvent(entry.Entity, (Entity)entry.CurrentValues.ToObject(), AuditEvent.EntityUpdated));
                        }

                        break;

                    case EntityState.Deleted:
                        if (entry.Entity is IHasDomainEvent @deletedEntity)
                        {
                            @deletedEntity.DomainEvents.Add(new AuditEntityEvent(entry.Entity, (Entity)entry.CurrentValues.ToObject(), AuditEvent.EntityRemoved));
                        }

                        break;
                }
            }

            //Encrypt Entries with Sensitive attribute
            //foreach (var entry in ChangeTracker.Entries().Where(entry => entry.State != EntityState.Unchanged))
            //{
            //    foreach (var prop in entry.Entity.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(SensitiveAttribute))))
            //    {
            //        var value = prop.GetValue(entry.Entity) as string ?? JsonConvert.SerializeObject(prop.GetValue(entry.Entity.GetType()), Formatting.Indented, new JsonSerializerSettings()
            //        {
            //            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //        });
            //        prop.SetValue(entry.Entity, await _cryptographyService.Encrypt(value).ConfigureAwait(false));
            //    }
            //}

            var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            await DispatchEvents().ConfigureAwait(false);
            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.RemovePluralizingTableNameConvention();
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity).ConfigureAwait(false);
            }
        }
    }
}

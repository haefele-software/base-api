using ProjectName.Application.Domain.Common;
using System.Threading.Tasks;

namespace Clean.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}

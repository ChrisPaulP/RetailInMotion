using RetailInMotion.Domain.Common;

namespace RetailInMotion.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
using Microsoft.EntityFrameworkCore;
using RetailInMotion.Application.Common.Interfaces;
using RetailInMotion.Domain.Common;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.Entities.Inventory;
using System.Reflection;

namespace RetailInMotion.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
        }
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<Product> Products => Set<Product>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            var events = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
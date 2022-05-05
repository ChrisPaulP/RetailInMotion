using Microsoft.EntityFrameworkCore;
using RetailInMotion.Domain.Entities;
using RetailInMotion.Domain.Entities.Inventory;

namespace RetailInMotion.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
           .Property<DateTime>("_orderDate")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("OrderDate")
           .IsRequired();

        builder.Property<bool>("_isCancelled").HasColumnName("IsCancelled");
        builder
            .OwnsOne(b => b.DeliveryAddress);

        builder.Ignore(e => e.DomainEvents);

        var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems));

        // DDD Patterns comment:
        //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}

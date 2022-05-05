using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailInMotion.Domain.Entities;

namespace RetailInMotion.Infrastructure.Persistence.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property<int>("OrderId")
           .IsRequired();

        builder.Property<int>("ProductId")
           .IsRequired();

        builder
            .Property<string>("_productName")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("ProductName")
            .IsRequired();

        builder
           .Property<decimal>("_itemPrice")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("ItemPrice")
           .HasColumnType("decimal(18,4)")
           .IsRequired();

        builder
            .Property<int>("_quantity")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("Quantity")
            .IsRequired();

        builder.Ignore(e => e.DomainEvents);
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailInMotion.Domain.Entities.Inventory;

namespace RetailInMotion.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
           .Property<int>("_quantity")
           .UsePropertyAccessMode(PropertyAccessMode.Field)
           .HasColumnName("Quantity")
           .IsRequired();
    }
}

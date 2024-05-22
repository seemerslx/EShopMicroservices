﻿using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext(DbContextOptions<DiscountContext> opts) : DbContext(opts)
    {
        public DbSet<Coupon> Coupons => Set<Coupon>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>()
                .HasData(new Coupon[]
                {
                    new Coupon() { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                    new Coupon() { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 },
                });
        }
    }
}
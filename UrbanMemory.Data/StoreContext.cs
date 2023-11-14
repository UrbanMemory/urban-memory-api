﻿using UrbanMemory.Domain.Catelog;
using UrbanMemory.Domain.Orders; 
using Microsoft.EntityFrameworkCore;

namespace UrbanMemory.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext() {

        }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            DbInitializer.Initialize(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite("Data Source = ../Registrar.sqlite", b => b.MigrationsAssembly("UrbanMemory.Api"));
            }
        }
    }
}
﻿using Microsoft.EntityFrameworkCore;
using Oplog.Persistence.Models;

namespace Oplog.Persistence
{
    public class OplogDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ConfiguredType> ConfiguredTypes { get; set; }

        public OplogDbContext(DbContextOptions<OplogDbContext> context):base(context)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
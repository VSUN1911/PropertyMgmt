using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RHStaging.Models;

namespace RHStaging.Data
{
    public class PropMgmtContext : DbContext
    {
        public PropMgmtContext (DbContextOptions<PropMgmtContext> options)
            : base(options)
        {
        }


        public DbSet<RHStaging.Models.Owner> Owners { get; set; } = default!;
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Renter> Renters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<Lease>().ToTable("Lease");
            modelBuilder.Entity<Owner>().ToTable("Owner");
            modelBuilder.Entity<Renter>().ToTable("Renter");
        }

    }
}

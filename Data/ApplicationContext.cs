using electronics.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace electronics.Data
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Computer> Computers { get; set; }
        public DbSet<ProductGalery> Galeries { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();  
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductGalery>()
                .HasOne(p => p.Computer)
                .WithMany(t => t.ProductGaleries)
                .HasForeignKey(p => p.ProductId);

           
        }
    }
}

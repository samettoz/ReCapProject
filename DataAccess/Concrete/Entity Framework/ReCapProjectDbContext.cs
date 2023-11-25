using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entity_Framework
{
    public class ReCapProjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server = DESKTOP-RBF7FR1\\SQLEXPRESS01; Database = ReCapProjectDb;Trusted_Connection = true;TrustServerCertificate=True");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .Property(u => u.PasswordSalt)
                .HasMaxLength(500);
            modelBuilder.Entity<User>()
                .Property(u => u.PasswordHash)
                .HasMaxLength(500);


        //    modelBuilder.Entity<Rental>()
        //         .HasOne(r => r.Car)
        //         .WithMany(c => c.Rentals)
        //         .HasForeignKey(r => r.CarId);

        //    modelBuilder.Entity<Rental>()
        //        .HasOne(r => r.Customer)
        //        .WithMany(c => c.Rentals)
        //        .HasForeignKey(c => c.CustomerId);

        //    modelBuilder.Entity<Car>()
        //        .HasOne(c => c.Brand)
        //        .WithMany(b => b.Cars)
        //        .HasForeignKey(c => c.BrandId);

        //    modelBuilder.Entity<Car>()
        //        .HasOne(c => c.Color)
        //        .WithMany(co => co.Cars)
        //        .HasForeignKey(c => c.ColorId);

        //    modelBuilder.Entity<Customer>()
        //        .HasOne(c => c.User)
        //        .WithMany(u => u.Customers)
        //        .HasForeignKey(c => c.UserId);
        }



        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaimDto> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Bson;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CRM3.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CRM3.Data
{
    public class CRMContext: IdentityDbContext<User>
    {
        public CRMContext(DbContextOptions<CRMContext> opt)
            : base(opt)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public DbSet<CustomerAccountProduct> CustomerAccountProducts { get; set; }
        public DbSet<Filial> Filials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Role { get; set; }
        //public DbSet<User> Users { get; set; }
       // public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomerAccountProduct>()
                .HasKey(cap => new { cap.CustomerAccoountId, cap.ProductId });
            modelBuilder.Entity<CustomerAccountProduct>()
                .HasOne(cap => cap.CustomerAccount)
                .WithMany(ca => ca.CustomerAccountProducts)
                .HasForeignKey(ca => ca.CustomerAccoountId);
            modelBuilder.Entity<CustomerAccountProduct>()
                .HasOne(cap => cap.Product)
                .WithMany(ca => ca.CustomerAccountProducts)
                .HasForeignKey(ca => ca.ProductId);

            /*modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.RoleId, ur.UserId });
            modelBuilder.Entity<UserRole>()
                .HasOne(cap => cap.User)
                .WithMany(ca => ca.UserRoles)
                .HasForeignKey(ca => ca.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(cap => cap.Role)
                .WithMany(ca => ca.UserRoles)
                .HasForeignKey(ca => ca.RoleId);
            */
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<CustomerAccount>().ToTable("CustomerAccount");
            modelBuilder.Entity<CustomerAccountProduct>().ToTable("CustomerAccountProduct");
            modelBuilder.Entity<Filial>().ToTable("Filial");
            modelBuilder.Entity<Product>().ToTable("Product");
            /*modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserRole>().ToTable("UserRole");*/

        }
    }
}

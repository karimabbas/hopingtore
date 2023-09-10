using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PwdHasher;
using ShopingStore.Migrations;
using ShopingStore.Models;

namespace ShopingStore.Data
{
    public class MyDBContext : IdentityDbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
        }

        // public override int SaveChanges()
        // {
        //     var Entities = from e in ChangeTracker.Entries()
        //                    where e.State == EntityState.Modified || e.State == EntityState.Added
        //                    select e.Entity;

        //     foreach (var Entity in Entities)
        //     {
        //         ValidationContext validationContext = new(Entity);
        //         Validator.ValidateObject(Entity, validationContext, true);
        //     }

        //     return base.SaveChanges();
        // }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>(role =>
            {
                role.HasData(new IdentityRole()
                {
                    Id = "b421e928-0613-9ebd-a64c-f10b6a706e73",
                    Name = "admin",
                    NormalizedName = "Admin",
                });
            });

            builder.Entity<IdentityUser>(user =>
            {

                var x = new IdentityUser
                {
                    Id = "22e40406-8a9d-2d82-912c-5d6a640ee696",
                    UserName = "Admin@gmail.com",
                    Email = "Admin@gmail.com",
                };
                PasswordHasher<IdentityUser> p = new PasswordHasher<IdentityUser>();
                x.PasswordHash = p.HashPassword(x, "123456Aa*");

                user.HasData(new IdentityUser()
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    PasswordHash = x.PasswordHash
                });

            });
            base.OnModelCreating(builder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<UserWishList> UserWishLists {get;set;}
        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderDetails> OrderDetails {get;set;}
        public DbSet<Payment> Payments {get;set;}
        public DbSet<Shipping> Shippings {get;set;}

    }

}
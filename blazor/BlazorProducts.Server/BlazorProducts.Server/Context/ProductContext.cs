
using BlazorProducts.Server.Context.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Server.Context
{
    public class ProductContext : IdentityDbContext<IdentityUser>
    {
        public ProductContext(DbContextOptions options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                      new Category { Id = 1, Name = "Books", Url = "books", Icon = "book" },
                      new Category { Id = 2, Name = "Electronics", Url = "electronics", Icon = "camera-slr" },
                      new Category { Id = 3, Name = "Video Games", Url = "video-games", Icon = "aperture" }
                  );
            modelBuilder.Entity<Product>().HasData(
               new Product
               {
                   Id = 1,
                   CategoryId = 1,
                   Title = "The Hitchhiker's Guide to the Galaxy",
                   Description = "The Hitchhiker's Guide to the Galaxy (sometimes referred to as HG2G, HHGTTG, H2G2, or tHGttG) is a comedy science fiction series created by Douglas Adams.",
                   Image = "https://upload.wikimedia.org/wikipedia/en/b/bd/H2G2_UK_front_cover.jpg",
                   DateCreated = new DateTime(2021, 1, 1)
               },
             
               new Product
               {
                   Id = 6,
                   CategoryId = 2,
                   Title = "Super Nintendo Entertainment System",
                   Description = "The Super Nintendo Entertainment System (SNES), also known as the Super NES or Super Nintendo, is a 16-bit home video game console developed by Nintendo that was released in 1990 in Japan and South Korea.",
                   Image = "https://upload.wikimedia.org/wikipedia/commons/e/ee/Nintendo-Super-Famicom-Set-FL.jpg",
                   DateCreated = new DateTime(2021, 1, 1)
               },
               new Product
               {
                   Id = 9,
                   CategoryId = 3,
                   Title = "Day of the Tentacle",
                   Description = "Day of the Tentacle, also known as Maniac Mansion II: Day of the Tentacle, is a 1993 graphic adventure game developed and published by LucasArts. It is the sequel to the 1987 game Maniac Mansion.",
                   Image = "https://upload.wikimedia.org/wikipedia/en/7/79/Day_of_the_Tentacle_artwork.jpg",
                   DateCreated = new DateTime(2021, 1, 1)
               }
           );

            /* modelBuilder.ApplyConfiguration(new RoleConfiguration());*/
            /* modelBuilder.Entity<EditionProduct>().HasKey(sc => new { sc.ProductId, sc.EditionId });*/
            modelBuilder.Entity<Edition>().HasData(
            new Edition { Id = 1, Name = "Default" },
            new Edition { Id = 2, Name = "Paperback" },
            new Edition { Id = 3, Name = "E-books" },
             new Edition { Id = 4, Name = "Audio-books" },
            new Edition { Id = 5, Name = "Pc" },
            new Edition { Id = 6, Name = "Playstation" },
            new Edition { Id = 7, Name = "X-box" }
        );
            /*modelBuilder.SharedTypeEntity<Dictionary<string, object>>("EditionProduct")
                .HasData(
                  new { EditionsId = 1, ProductsId = 1 },
                  new { EditionsId = 2, ProductsId = 1 },
                  new { EditionsId = 3, ProductsId = 1 }
                );*/
            modelBuilder.Entity<ProductVariant>()
                .HasKey(p => new { p.ProductId, p.EditionId });
            modelBuilder.Entity<ProductVariant>()
                .HasData(
                new ProductVariant { ProductId = 1, EditionId = 1, Price = 9, OriginalPrice = 19 },
                new ProductVariant { ProductId = 1, EditionId = 2, Price = 12 },
                new ProductVariant { ProductId = 1, EditionId = 3, Price = 15},

                 new ProductVariant { ProductId = 6, EditionId = 1, Price = 9, OriginalPrice = 19 },
                new ProductVariant { ProductId = 6, EditionId = 2, Price = 12 },
                new ProductVariant { ProductId = 6, EditionId = 3, Price = 15 },

                 new ProductVariant { ProductId = 9, EditionId = 1, Price = 9, OriginalPrice = 19 },
                new ProductVariant { ProductId = 9, EditionId = 2, Price = 12 },
                new ProductVariant { ProductId = 9, EditionId = 3, Price = 15 }

                );
        }

    
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Edition> Editions { get; set; }
    }
}

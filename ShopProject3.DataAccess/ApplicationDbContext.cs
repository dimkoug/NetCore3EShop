using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopProject3.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject3.DataAccess
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
        {
        }

        public virtual DbSet<BannersEntity> Banners { get; set; }
        public virtual DbSet<BannersStatisticsEntity> BannersStatistics { get; set; }
        public virtual DbSet<OffersEntity> Offers { get; set; }
        public virtual DbSet<OffersDetailEntity> OffersDetail { get; set; }
        public virtual DbSet<PressReleasesEntity> PressReleases { get; set; }
        public virtual DbSet<EventsEntity> Events { get; set; }
        public virtual DbSet<EventsMediaEntity> EventsMedia { get; set; }

        public virtual DbSet<MediaEntity> Media { get; set; }


        public virtual DbSet<BrandsEntity> Brands { get; set; }

        public virtual DbSet<TagsEntity> Tags { get; set; }

        public virtual DbSet<CategoriesEntity> Categories { get; set; }


        public virtual DbSet<FeaturesEntity> Features { get; set; }

        public virtual DbSet<FeatureAttributesEntity> FeatureAttributes { get; set; }

        public virtual DbSet<CategoryFeaturesEntity> CategoryFeatures { get; set; }

        public virtual DbSet<ProductsEntity> Products { get; set; }

        public virtual DbSet<ShoppingCartItemsEntity> ShoppingCartItems { get; set; }

        public virtual DbSet<ProductCategoriesEntity> ProductCategories { get; set; }

        public virtual DbSet<ProductMediaEntity> ProductMedia { get; set; }

        public virtual DbSet<ProductFeatureAttributesEntity> ProductFeatureAttributes { get; set; }

        public virtual DbSet<OrdersEntity> Orders { get; set; }

        public virtual DbSet<OrdersDetailEntity> OrdersDetail { get; set; }

        public virtual DbSet<ProductTagsEntity> ProductTags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductCategoriesEntity>()
                .HasKey(bc => new { bc.CategoryId, bc.ProductId });
            modelBuilder.Entity<ProductFeatureAttributesEntity>()
                .HasKey(bc => new { bc.FeatureAttributeId, bc.ProductId });
            modelBuilder.Entity<ProductMediaEntity>()
                .HasKey(bc => new { bc.MediaId, bc.ProductId });
            modelBuilder.Entity<ProductTagsEntity>()
                .HasKey(bc => new { bc.TagId, bc.ProductId });
            modelBuilder.Entity<CategoryFeaturesEntity>()
                .HasKey(bc => new { bc.CategoryId, bc.FeatureId });

            modelBuilder.Entity<ProductsEntity>()
                 .Property(p => p.Price)
                 .HasColumnType("decimal(19,10)");

            modelBuilder.Entity<OrdersEntity>()
     .Property(p => p.OrderTotal)
     .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OffersDetailEntity>()
     .Property(p => p.Price)
     .HasColumnType("decimal(19,10)");

        }
    }
}

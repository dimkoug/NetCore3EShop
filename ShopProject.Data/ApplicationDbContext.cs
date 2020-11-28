using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: base(options)
        {
        }

        public virtual DbSet<AppointmentEntity> Appointments { get; set; }
        public virtual DbSet<BannerEntity> Banners { get; set; }
        public virtual DbSet<BannerStatisticEntity> BannerStatistics { get; set; }

        public virtual DbSet<BlogCategoryEntity> BlogCategories { get; set; }

        public virtual DbSet<BlogPostCategoryEntity> BlogPostCategories { get; set; }

        public virtual DbSet<BlogPostEntity> BlogPosts { get; set; }

        public virtual DbSet<BlogPostMediaEntity> BlogPostsMedia { get; set; }

        public virtual DbSet<BlogPostTagEntity> BlogPostTags { get; set; }


        public virtual DbSet<BrandEntity> Brands { get; set; }
        public virtual DbSet<EventEntity> Events { get; set; }

        public virtual DbSet<EventLocationEntity> EventLocations { get; set; }

        public virtual DbSet<EventMediaEntity> EventMedia { get; set; }

        public virtual DbSet<EventTagEntity> EventTags { get; set; }

        public virtual DbSet<MediaEntity> Media { get; set; }

        public virtual DbSet<OfferEntity> Offers { get; set; }

        public virtual DbSet<OfferDetailEntity> OfferDetails { get; set; }

        public virtual DbSet<OrderEntity> Orders { get; set; }

        public virtual DbSet<OrderDetailEntity> OrderDetails { get; set; }

        public virtual DbSet<PressReleaseEntity> PressReleases { get; set; }

        public virtual DbSet<ProductAttributeEntity> ProductAttributes { get; set; }

        public virtual DbSet<ProductEntity> Products { get; set; }

        public virtual DbSet<FeatureAttributeEntity> FeatureAttributes { get; set; }

        public virtual DbSet<FeatureEntity> Features { get; set; }

        public virtual DbSet<ProductMediaEntity> ProductMedia { get; set; }

        public virtual DbSet<ProductShopCategoryEntity> ProductShopCategories { get; set; }

        public virtual DbSet<ProductTagEntity> ProductTags { get; set; }

        public virtual DbSet<ShopCategoryEntity> ShopCategories { get; set; }

        public virtual  DbSet<ShopCategoryFeatureEntity> ShopCategoryFeatures { get; set; }

        public virtual DbSet<ShoppingCartItemEntity> ShoppingCartItems { get; set; }

        public virtual DbSet<TagEntity> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            modelBuilder
            .Entity<BannerEntity>()
            .Property(p => p.Location)
            .HasConversion(x => (int)x, x => (LocationType)x);


            modelBuilder.Entity<ProductTagEntity>()
    .HasKey(bc => new { bc.TagId, bc.ProductId });


            modelBuilder.Entity<BlogPostTagEntity>()
    .HasKey(bc => new { bc.BlogPostId, bc.TagId });

            modelBuilder.Entity<ProductMediaEntity>()
    .HasKey(bc => new { bc.MediaId, bc.ProductId });

            modelBuilder.Entity<BlogPostCategoryEntity>()
    .HasKey(bc => new { bc.BlogCategoryId, bc.BlogPostId });

            modelBuilder.Entity<BlogPostMediaEntity>()
    .HasKey(bc => new { bc.MediaId, bc.BlogPostId });

            modelBuilder.Entity<EventMediaEntity>()
    .HasKey(bc => new { bc.EventId, bc.MediaId });

            modelBuilder.Entity<EventTagEntity>()
    .HasKey(bc => new { bc.TagId, bc.EventId });




            modelBuilder.Entity<ProductShopCategoryEntity>()
                .HasKey(bc => new { bc.ShopCategoryId, bc.ProductId });
            modelBuilder.Entity<ProductAttributeEntity>()
                .HasKey(bc => new { bc.FeatureAttributesId, bc.ProductsId });
            modelBuilder.Entity<ProductMediaEntity>()
                .HasKey(bc => new { bc.MediaId, bc.ProductId });
            modelBuilder.Entity<ProductTagEntity>()
                .HasKey(bc => new { bc.TagId, bc.ProductId });
            modelBuilder.Entity<ShopCategoryFeatureEntity>()
                .HasKey(bc => new { bc.ShopCategoryId, bc.ProductFeatureId });

            modelBuilder.Entity<ProductEntity>()
                 .Property(p => p.Price)
                 .HasColumnType("decimal(19,10)");

            modelBuilder.Entity<OrderEntity>()
     .Property(p => p.OrderTotal)
     .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OfferDetailEntity>()
     .Property(p => p.Price)
     .HasColumnType("decimal(19,10)");

        }

    }
}

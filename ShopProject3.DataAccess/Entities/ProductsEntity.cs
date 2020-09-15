using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class ProductsEntity
    {
        public ProductsEntity()
        {
            OffersDetail = new HashSet<OffersDetailEntity>();
            OrdersDetail = new HashSet<OrdersDetailEntity>();
            ProductCategories = new HashSet<ProductCategoriesEntity>();
            ProductFeatureAttributes = new HashSet<ProductFeatureAttributesEntity>();
            ProductMedia = new HashSet<ProductMediaEntity>();
            ProductTags = new HashSet<ProductTagsEntity>();
            ShoppingCartItems = new HashSet<ShoppingCartItemsEntity>();
            Children = new HashSet<ProductsEntity>();
        }
        [Key]
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public int? BrandId { get; set; }
        public string Hero { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public bool Featured { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }

        public virtual BrandsEntity Brand { get; set; }
        public virtual ProductsEntity Parent { get; set; }
        public virtual ICollection<OffersDetailEntity> OffersDetail { get; set; }
        public virtual ICollection<OrdersDetailEntity> OrdersDetail { get; set; }
        public virtual ICollection<ProductCategoriesEntity> ProductCategories { get; set; }
        public virtual ICollection<ProductFeatureAttributesEntity> ProductFeatureAttributes { get; set; }
        public virtual ICollection<ProductMediaEntity> ProductMedia { get; set; }
        public virtual ICollection<ProductTagsEntity> ProductTags { get; set; }
        public virtual ICollection<ShoppingCartItemsEntity> ShoppingCartItems { get; set; }
        public virtual ICollection<ProductsEntity> Children  { get; set; }
    }
}

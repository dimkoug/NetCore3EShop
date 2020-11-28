using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class ProductEntity
    {
        public ProductEntity()
        {
            Children = new HashSet<ProductEntity>();
            ProductShopCategories = new HashSet<ProductShopCategoryEntity>();
            ProductMediaList = new HashSet<ProductMediaEntity>();
            ProductAttributes = new HashSet<ProductAttributeEntity>();
            ProductTags = new HashSet<ProductTagEntity>();
            ShoppingCartList = new HashSet<ShoppingCartItemEntity>();
            OrderDetails = new HashSet<OrderDetailEntity>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public int? ParentId { get; set; }

        public virtual ProductEntity  Parent { get; set; }

        [Required]
        public int BrandId { get; set; }

        public string? Hero { get; set; }

        [Required]
        public decimal Price { get; set; } = 0;

        public virtual BrandEntity Brand { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public bool Featured { get; set; } = false;

        public ICollection<ProductEntity> Children { get; set; }

        public ICollection<ProductShopCategoryEntity> ProductShopCategories { get; set; }

        public ICollection<ProductMediaEntity> ProductMediaList { get; set; }

        public ICollection<ProductAttributeEntity> ProductAttributes { get; set; }

        public ICollection<ProductTagEntity> ProductTags { get; set; }

        public ICollection<ShoppingCartItemEntity> ShoppingCartList { get; set; }

        public ICollection<OrderDetailEntity> OrderDetails { get; set; }


    }
}

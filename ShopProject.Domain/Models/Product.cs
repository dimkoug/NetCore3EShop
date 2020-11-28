using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ShopProject.Domain.Models
{
    public class Product
    {
        public Product()
        {
            Children = new HashSet<Product>();
            ProductShopCategories = new HashSet<ProductShopCategory>();
            ProductMediaList = new HashSet<ProductMedia>();
            ProductAttributes = new HashSet<ProductAttribute>();
            ProductTags = new HashSet<ProductTag>();
            ShoppingCartList = new HashSet<ShoppingCartItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }


        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public virtual Product Parent { get; set; }

        public int BrandId { get; set; }
        
        public virtual Brand Brand { get; set; }

        public string? Hero { get; set; }

        public decimal Price { get; set; } = 0;
       

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public bool Featured { get; set; } = false;

        public ICollection<Product> Children { get; set; }

        public ICollection<ProductShopCategory> ProductShopCategories { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }

        public ICollection<ProductMedia> ProductMediaList { get; set; }

        public ICollection<ProductAttribute> ProductAttributes { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }

        public IEnumerable<int> SelectedTags { get; set; }

        public ICollection<ShoppingCartItem> ShoppingCartList { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public ICollection<IFormFile> files { get; set; }

        [DisplayName("Hero")]
        public IFormFile file { get; set; }
    }
}

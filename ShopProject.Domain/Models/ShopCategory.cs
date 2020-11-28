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
    public class ShopCategory
    {
        public ShopCategory()
        {
            Children = new HashSet<ShopCategory>();
            CategoryFeatures = new HashSet<ShopCategoryFeature>();
            ProductShopCategories = new HashSet<ProductShopCategory>();
        }


        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? ParentId { get; set; }

        public virtual ShopCategory Parent { get; set; }

        public string? Hero { get; set; }


        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<ShopCategory> Children { get; set; }

        public ICollection<ShopCategoryFeature> CategoryFeatures { get; set; }

        public ICollection<ProductShopCategory> ProductShopCategories { get; set; }

        [DisplayName("Hero")]
        public IFormFile file { get; set; }
    }
}

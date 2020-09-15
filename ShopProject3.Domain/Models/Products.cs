using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Products
    {
        public Products()
        {
            ProductMedia = new HashSet<ProductMedia>();
            ProductTags = new HashSet<ProductTags>();
            ProductCategories = new HashSet<ProductCategories>();
            ProductFeatureAttributes = new HashSet<ProductFeatureAttributes>();
            Children = new HashSet<Products>();
         }

        public int Id { get; set; }

        public int BrandId { get; set; }

        public virtual Brands Brand { get; set; }

        public int? ParentId { get; set; }

        public virtual Products Parent { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public string? HeroImage { get; set; }

        public decimal Price { get; set; }

        public bool Deleted { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public virtual ICollection<ProductCategories> ProductCategories { get; set; }

        public virtual IEnumerable<int> SelectedCategories { get; set; }

        public virtual  ICollection<ProductFeatureAttributes>  ProductFeatureAttributes { get; set; }

        public virtual ICollection<ProductMedia> ProductMedia { get; set; }

        public virtual ICollection<ProductTags> ProductTags { get; set; }

        public virtual IEnumerable<int> SelectedTags { get; set; }

        public virtual ICollection<Products> Children { get; set; }

        public virtual ICollection<IFormFile> Photos { get; set; }



    }
}

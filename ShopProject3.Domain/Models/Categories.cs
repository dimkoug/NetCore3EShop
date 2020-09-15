using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Categories
    {
        public Categories()
        {
            Children = new HashSet<Categories>();
            ProductCategories = new HashSet<ProductCategories>();
            CategoryFeatures = new HashSet<CategoryFeatures>();
        }

        public int Id { get; set; }

        public int? ParentId { get; set; }

        public virtual Categories Parent { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Categories> Children { get; set; }

        public virtual ICollection<ProductCategories> ProductCategories { get; set; }

        public virtual ICollection<CategoryFeatures> CategoryFeatures { get; set; }
    }
}

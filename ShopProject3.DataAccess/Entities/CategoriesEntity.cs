using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public  class CategoriesEntity
    {
        public CategoriesEntity()
        {
            CategoryFeatures = new HashSet<CategoryFeaturesEntity>();
            InverseParent = new HashSet<CategoriesEntity>();
            ProductCategories = new HashSet<ProductCategoriesEntity>();
            Products = new HashSet<ProductsEntity>();
        }
        [Key]
        public int Id { get; set; }
        public string Hero { get; set; }
        public int? ParentId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }

        public virtual CategoriesEntity Parent { get; set; }
        public virtual ICollection<CategoryFeaturesEntity> CategoryFeatures { get; set; }
        public virtual ICollection<CategoriesEntity> InverseParent { get; set; }
        public virtual ICollection<ProductCategoriesEntity> ProductCategories { get; set; }
        public virtual ICollection<ProductsEntity> Products { get; set; }
    }
}

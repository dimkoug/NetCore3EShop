using System;
using System.Collections.Generic;

namespace ShopProject3.DataAccess.Entities
{
    public class CategoryFeaturesEntity
    {
        public int FeatureId { get; set; }
        public int CategoryId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual CategoriesEntity Category { get; set; }
        public virtual FeaturesEntity Feature { get; set; }
    }
}

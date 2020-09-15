using System;
using System.Collections.Generic;

namespace ShopProject3.DataAccess.Entities
{
    public class ProductFeatureAttributesEntity
    {
        public int ProductId { get; set; }
        public int FeatureAttributeId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual FeatureAttributesEntity FeatureAttribute { get; set; }
        public virtual ProductsEntity Product { get; set; }
    }
}

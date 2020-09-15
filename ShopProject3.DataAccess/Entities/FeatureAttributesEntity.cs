using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class FeatureAttributesEntity
    {
        public FeatureAttributesEntity()
        {
            ProductFeatureAttributes = new HashSet<ProductFeatureAttributesEntity>();
        }

        [Key]
        public int Id { get; set; }
        public int FeatureId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }

        public virtual FeaturesEntity Feature { get; set; }
        public virtual ICollection<ProductFeatureAttributesEntity> ProductFeatureAttributes { get; set; }
    }
}

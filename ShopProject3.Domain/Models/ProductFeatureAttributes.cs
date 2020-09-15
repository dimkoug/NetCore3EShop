using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class ProductFeatureAttributes
    {


        public int ProductId { get; set; }

        public virtual Products Product { get; set; }

        public int FeatureAttributeId { get; set; }

        public virtual FeatureAttributes FeatureAttribute { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}

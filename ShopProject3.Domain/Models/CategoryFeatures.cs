using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class CategoryFeatures
    {
        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public int FeatureId { get; set; }

        public virtual Features Feature { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class ProductAttribute
    {
        public int FeatureAttributesId { get; set; }

        public virtual FeatureAttribute FeatureAttributes { get; set; }

        public int ProductsId { get; set; }

        public virtual Product Products { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

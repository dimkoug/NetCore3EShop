using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Features
    {
        public Features()
        {
            FeatureAttributes = new HashSet<FeatureAttributes>();
            CategoryFeatures = new HashSet<CategoryFeatures>();
        }

        
        public int Id { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<FeatureAttributes> FeatureAttributes { get; set; }

        public virtual ICollection<CategoryFeatures> CategoryFeatures { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }
    }
}

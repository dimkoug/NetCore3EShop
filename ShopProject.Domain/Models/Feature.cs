using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class Feature
    {
        public Feature()
        {
            FeatureAttributes = new HashSet<FeatureAttribute>();
            ShopCategoryFeatures = new HashSet<ShopCategoryFeature>();
        }


        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<FeatureAttribute> FeatureAttributes { get; set; }

        public ICollection<ShopCategoryFeature> ShopCategoryFeatures { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }
    }
}

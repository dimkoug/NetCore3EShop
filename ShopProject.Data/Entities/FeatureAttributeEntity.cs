using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class FeatureAttributeEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FeatureId { get; set; }

        public virtual FeatureEntity Feature { get; set; }

        [Required]
        public string Title { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;
    }
}

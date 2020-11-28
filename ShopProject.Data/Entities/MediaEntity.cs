using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class MediaEntity
    {

        public MediaEntity()
        {
            Products = new HashSet<ProductMediaEntity>();
            Posts = new HashSet<BlogPostMediaEntity>();
            Events = new HashSet<EventMediaEntity>();
        }
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string MediaPath { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<ProductMediaEntity> Products { get; set; }

        public ICollection<BlogPostMediaEntity> Posts { get; set; }

        public ICollection<EventMediaEntity> Events { get; set; }
    }
}

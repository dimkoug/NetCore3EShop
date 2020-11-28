using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class TagEntity
    {

        public TagEntity()
        {
            Events = new HashSet<EventTagEntity>();
            Posts = new HashSet<BlogPostTagEntity>();
            Products = new HashSet<ProductTagEntity>();

        }
        
        
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<EventTagEntity> Events { get; set; }

        public ICollection<BlogPostTagEntity> Posts { get; set; }

        public ICollection<ProductTagEntity> Products { get; set; }
    }
}

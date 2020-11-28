using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class Tag
    {
        public Tag()
        {
            Events = new HashSet<EventTag>();
            Posts = new HashSet<BlogPostTag>();
            Products = new HashSet<ProductTag>();

        }


        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<EventTag> Events { get; set; }

        public ICollection<BlogPostTag> Posts { get; set; }

        public ICollection<ProductTag> Products { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class Media
    {
        public Media()
        {
            Products = new HashSet<ProductMedia>();
            Posts = new HashSet<BlogPostMedia>();
            Events = new HashSet<EventMedia>();
        }

        
        public int Id { get; set; }

        [Required]
        public string MediaPath { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<ProductMedia> Products { get; set; }

        public ICollection<BlogPostMedia> Posts { get; set; }

        public ICollection<EventMedia> Events { get; set; }

        [DisplayName("Έγγραφα")]
        public IFormFile file { get; set; }
    }
}

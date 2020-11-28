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
    public class BlogCategory
    {
        public BlogCategory()
        {
            Children = new HashSet<BlogCategory>();
            BlogPostCategories = new HashSet<BlogPostCategory>();
        }

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? ParentId { get; set; }

        public virtual BlogCategory Parent { get; set; }

        public string? Hero { get; set; }


        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<BlogCategory> Children { get; set; }

        public ICollection<BlogPostCategory> BlogPostCategories { get; set; }

        [DisplayName("Hero")]
        public IFormFile file { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class BlogPost
    {
        public BlogPost()
        {
            BlogPostCategories = new HashSet<BlogPostCategory>();

            BlogPostTags = new HashSet<BlogPostTag>();

            BlogPostMedia = new HashSet<BlogPostMedia>();
        }


  
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<BlogPostCategory> BlogPostCategories { get; set; }

        public IEnumerable<int> SelectedCategories { get; set; }

        public ICollection<BlogPostTag> BlogPostTags { get; set; }

        public IEnumerable<int> SelectedTags { get; set; }

        public ICollection<BlogPostMedia> BlogPostMedia { get; set; }

        public ICollection<IFormFile> files { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class BlogPostEntity
    {

        public BlogPostEntity()
        {
            BlogPostCategories = new HashSet<BlogPostCategoryEntity>();

            PostTags = new HashSet<BlogPostTagEntity>();

            BlogPostMedia = new HashSet<BlogPostMediaEntity>();
        }
        
        
        [Key]
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

        public ICollection<BlogPostCategoryEntity> BlogPostCategories { get; set; }

        public ICollection<BlogPostTagEntity> PostTags { get; set; }

        public ICollection<BlogPostMediaEntity> BlogPostMedia { get; set; }

    }
}

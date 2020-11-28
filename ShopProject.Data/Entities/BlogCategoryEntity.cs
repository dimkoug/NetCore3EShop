using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class BlogCategoryEntity
    {
        public BlogCategoryEntity()
        {
            Children = new HashSet<BlogCategoryEntity>();
            BlogPostCategories = new HashSet<BlogPostCategoryEntity>();
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int? ParentId { get; set; }

        public virtual BlogCategoryEntity Parent { get; set; }

        public string? Hero { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<BlogCategoryEntity> Children { get; set; }

        public ICollection<BlogPostCategoryEntity> BlogPostCategories { get; set; }
    }
}

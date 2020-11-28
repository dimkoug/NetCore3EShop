using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class BlogPostCategoryEntity
    {
        public int BlogPostId { get; set; }

        public virtual BlogPostEntity BlogPost { get; set; }

        public int BlogCategoryId { get; set; }

        public virtual BlogCategoryEntity BlogCategory { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

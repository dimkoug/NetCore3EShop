using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class BlogPostTagEntity
    {
        public int BlogPostId { get; set; }

        public virtual BlogPostEntity BlogPost { get; set; }

        public int TagId { get; set; }

        public virtual TagEntity Tag { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

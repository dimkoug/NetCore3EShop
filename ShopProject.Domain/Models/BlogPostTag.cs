using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class BlogPostTag
    {
        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

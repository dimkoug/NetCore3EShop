using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class BlogPostMedia
    {
        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public int MediaId { get; set; }

        public virtual Media Media { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class ProductShopCategory
    {
        public int ShopCategoryId { get; set; }

        public virtual ShopCategory ShopCategory { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

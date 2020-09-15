using System;
using System.Collections.Generic;

namespace ShopProject3.DataAccess.Entities
{
    public class ProductMediaEntity
    {
        public int ProductId { get; set; }
        public int MediaId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual MediaEntity Media { get; set; }
        public virtual ProductsEntity Product { get; set; }
    }
}

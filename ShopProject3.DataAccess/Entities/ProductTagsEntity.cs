using System;
using System.Collections.Generic;

namespace ShopProject3.DataAccess.Entities
{
    public partial class ProductTagsEntity
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ProductsEntity Product { get; set; }
        public virtual TagsEntity Tag { get; set; }
    }
}

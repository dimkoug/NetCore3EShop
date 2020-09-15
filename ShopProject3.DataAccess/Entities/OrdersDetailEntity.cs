using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class OrdersDetailEntity
    {
        [Key]
        public int Id { get; set; }
        public int OrdersId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual OrdersEntity Orders { get; set; }
        public virtual ProductsEntity Product { get; set; }
    }
}

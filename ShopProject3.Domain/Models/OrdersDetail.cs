using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class OrdersDetail
    {
        public int Id { get; set; }

        public int OrdersId { get; set; }

        public virtual Orders Orders { get; set; }

        public int ProductId { get; set; }

        public virtual Products Product { get; set; }

        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

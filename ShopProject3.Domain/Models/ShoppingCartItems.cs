using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class ShoppingCartItems
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Products Product { get; set; }

        public int Quantity { get; set; }

        public string SessionCartId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

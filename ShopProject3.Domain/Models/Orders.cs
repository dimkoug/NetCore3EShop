using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrdersDetail>();
        }

        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<OrdersDetail> OrderDetails { get; set; }
    }
}

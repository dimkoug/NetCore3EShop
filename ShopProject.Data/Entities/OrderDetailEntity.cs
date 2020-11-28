using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class OrderDetailEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public virtual OrderEntity Order { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}

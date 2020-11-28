using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class ShoppingCartItemEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        public string SessionCartId { get; set; }

        [Required]
        public int Quantity { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

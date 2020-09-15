using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.DataAccess.Entities
{
    public class ShoppingCartItemsEntity
    {
        [Key]

        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual ProductsEntity Product { get; set; }

        public int Quantity { get; set; }

        public string SessionCartId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}

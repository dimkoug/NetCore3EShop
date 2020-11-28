using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class OfferDetailEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OfferId { get; set; }

        public virtual OfferEntity Offer { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        [Required]
        public decimal Price { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;
    }
}

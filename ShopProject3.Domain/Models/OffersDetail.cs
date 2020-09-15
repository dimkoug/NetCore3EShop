using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class OffersDetail
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public int ProductsId { get; set; }
        public int OffersId { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Published { get; set; }
        public bool Deleted { get; set; }

        public virtual Products Products { get; set; }
        public virtual Offers Offers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class OffersDetailEntity
    {
        [Key]
        public int Id { get; set; }
        public int Order { get; set; }
        public int ProductsId { get; set; }
        public int OffersId { get; set; }
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }

        public virtual ProductsEntity Products { get; set; }
        public virtual OffersEntity Offers { get; set; }
    }
}

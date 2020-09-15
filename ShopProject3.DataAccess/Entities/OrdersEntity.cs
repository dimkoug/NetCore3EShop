using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class OrdersEntity
    {
        public OrdersEntity()
        {
            OrdersDetail = new HashSet<OrdersDetailEntity>();
        }

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string PhoneNumber { get; set; }
        public decimal? OrderTotal { get; set; }
        public DateTime? OrderPlaced { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<OrdersDetailEntity> OrdersDetail { get; set; }
    }
}

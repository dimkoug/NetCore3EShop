using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        
        public int Id { get; set; }



        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string State { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; } = DateTime.Now;


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

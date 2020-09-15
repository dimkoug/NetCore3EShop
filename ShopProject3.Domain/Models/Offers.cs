using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Offers
    {
        public Offers()
        {
            OffersDetail = new HashSet<OffersDetail>();
        }


        
        public int Id { get; set; }
        public int Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OffersDetail> OffersDetail { get; set; }
    }
}

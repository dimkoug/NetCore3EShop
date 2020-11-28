using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class Offer
    {
        public Offer()
        {
            OfferDetails = new HashSet<OfferDetail>();
        }

        
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }


        [Required]
        public DateTime EndDate { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<OfferDetail> OfferDetails { get; set; }
    }
}

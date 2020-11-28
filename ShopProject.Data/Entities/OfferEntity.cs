using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class OfferEntity
    {
        public OfferEntity()
        {
            OfferDetails = new HashSet<OfferDetailEntity>();
        }

        [Key]
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

        public ICollection<OfferDetailEntity> OfferDetails { get; set; }

    }
}

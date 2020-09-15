using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class OffersEntity
    {
        public OffersEntity()
        {
            OffersDetail = new HashSet<OffersDetailEntity>();
        }


        [Key]
        public int Id { get; set; }
        public int Order { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool? Published { get; set; }
        public bool? Deleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OffersDetailEntity> OffersDetail { get; set; }
    }
}

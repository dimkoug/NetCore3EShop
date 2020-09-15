using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Banners
    {
        public Banners()
        {
            BannersStatistics = new HashSet<BannersStatistics>();
        }

        public int Id { get; set; }
        public int Order { get; set; }
        public int Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerCode { get; set; }

        public virtual ICollection<BannersStatistics> BannersStatistics { get; set; }

        [DisplayName("Banner")]
        public IFormFile file { get; set; }
    }
}

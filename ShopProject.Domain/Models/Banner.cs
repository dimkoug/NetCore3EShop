using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{

    public enum LocationType
    {
        TOP=0,
        BOTTOM=1,
        LEFT=2,
        RIGHT=3
    }



    public class Banner
    {
        public Banner()
        {
            Statistics = new HashSet<BannerStatistic>();
        }


        
        public int Id { get; set; }

        public string? Url { get; set; }

        public LocationType Location { get; set; }

        public int Order { get; set; }

        public string? Code { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<BannerStatistic> Statistics { get; set; }

        [DisplayName("Banner")]
        public IFormFile file { get; set; }
    }
}

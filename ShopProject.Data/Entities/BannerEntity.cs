using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{

    public enum LocationType
    {
        TOP = 0,
        BOTTOM = 1,
        LEFT = 2,
        RIGHT = 3
    }

    public class BannerEntity
    {

        public BannerEntity()
        {
            Statistics = new HashSet<BannerStatisticEntity>();
        }
        
        
        [Key]
        public int Id { get; set; }

        public LocationType Location { get; set; }


        public string? Url { get; set; }

        public int Order { get; set; }

        public string? Code { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<BannerStatisticEntity> Statistics { get; set; }
    }
}

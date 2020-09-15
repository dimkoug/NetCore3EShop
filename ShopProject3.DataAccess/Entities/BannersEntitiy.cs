using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public  class BannersEntity
    {
        public BannersEntity()
        {
            BannersStatistics = new HashSet<BannersStatisticsEntity>();
        }
        [Key]
        public int Id { get; set; }
        public int Order { get; set; }
        public int Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string BannerImagePath { get; set; }
        public string BannerCode { get; set; }

        public virtual ICollection<BannersStatisticsEntity> BannersStatistics { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class BannersStatisticsEntity
    {
        [Key]
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int BannersId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual BannersEntity Banners { get; set; }
    }
}

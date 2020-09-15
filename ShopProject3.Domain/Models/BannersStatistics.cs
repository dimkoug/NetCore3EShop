using System;
using System.Collections.Generic;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class BannersStatistics
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public int BannersId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual Banners Banners { get; set; }
    }
}

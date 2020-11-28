using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class BannerStatisticEntity
    {
        [Key]
        public int Id { get; set; }

        public string? SessionId { get; set; }

        [Required]
        public int BannerId { get; set; }

        public virtual BannerEntity Banner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}

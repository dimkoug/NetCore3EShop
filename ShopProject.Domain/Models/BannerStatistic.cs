using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class BannerStatistic
    {
 
        public int Id { get; set; }

        public string? SessionId { get; set; }

        [Required]
        public int BannerId { get; set; }

        public virtual Banner Banner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

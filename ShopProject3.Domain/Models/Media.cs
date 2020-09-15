using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Media
    {
        public Media()
        {
            Products = new HashSet<ProductMedia>();
        }

        public int Id { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public string MediaPath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<ProductMedia> Products { get; set; }

        [DisplayName("Έγγραφα")]
        public IFormFile file { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class Brands
    {

        public Brands()
        {
            Products = new HashSet<Products>();
        }
        
        public int Id { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<Products> Products { get; set; }
    }
}

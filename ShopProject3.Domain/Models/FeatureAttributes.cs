using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class FeatureAttributes
    {
        public int Id { get; set; }

        public int FeatureId { get; set; }

        public  virtual Features Feature { get; set; }

        public int Order { get; set; }

        public bool Published { get; set; }

        public bool Deleted { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

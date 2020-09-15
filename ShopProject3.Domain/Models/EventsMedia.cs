using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ShopProject3.Domain.Models
{
    public class EventsMedia
    {
        public int Id { get; set; }
        public int EventsId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string MediaPath { get; set; }

        public virtual Events Events { get; set; }

        [DisplayName("Έγγραφα")]
        public IFormFile file { get; set; }
    }
}

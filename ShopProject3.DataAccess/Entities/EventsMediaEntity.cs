using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class EventsMediaEntity
    {
        [Key]
        public int Id { get; set; }
        public int EventsId { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string MediaPath { get; set; }

        public virtual EventsEntity Events { get; set; }
    }
}

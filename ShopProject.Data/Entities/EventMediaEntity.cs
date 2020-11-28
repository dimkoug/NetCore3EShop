using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Data.Entities
{
    public class EventMediaEntity
    {
        public int EventId { get; set; }

        public virtual EventEntity Event { get; set; }

        public int MediaId { get; set; }

        public virtual MediaEntity Media { get; set; }

        public int Order { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}

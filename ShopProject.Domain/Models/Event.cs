using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Domain.Models
{
    public class Event
    {
        public Event()
        {
            EventMedia = new HashSet<EventMedia>();
            EventTags = new HashSet<EventTag>();
        }

            public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int EventLocationId { get; set; }

        public virtual EventLocation EventLocation { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }


        [Required]
        public DateTime EndDate { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool Published { get; set; } = false;

        public bool Deleted { get; set; } = false;

        public ICollection<EventMedia> EventMedia { get; set; }

        public ICollection<EventTag> EventTags { get; set; }

        public IEnumerable<int> SelectedTags { get; set; }

        public ICollection<IFormFile> files { get; set; }
    }
}

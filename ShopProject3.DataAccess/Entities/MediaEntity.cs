using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class MediaEntity
    {
        public MediaEntity()
        {
            ProductMedia = new HashSet<ProductMediaEntity>();
        }

        [Key]
        public int Id { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string MediaPath { get; set; }

        public virtual ICollection<ProductMediaEntity> ProductMedia { get; set; }
    }
}

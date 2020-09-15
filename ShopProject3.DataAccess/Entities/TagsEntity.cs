using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopProject3.DataAccess.Entities
{
    public class TagsEntity
    {
        public TagsEntity()
        {
            ProductTags = new HashSet<ProductTagsEntity>();
        }

        [Key]
        public int Id { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Published { get; set; }
        public bool Deleted { get; set; }
        public string Title { get; set; }

        public virtual ICollection<ProductTagsEntity> ProductTags { get; set; }
    }
}

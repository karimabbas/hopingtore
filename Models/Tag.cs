using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class Tag
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string? Tag_name { get; set; }
        public string? Tag_Description { get; set; }
        public ICollection<ProductTag>? ProductTags { get; set; }
        public virtual ICollection<BlogTag>? BlogTag { get; set; }


    }
}
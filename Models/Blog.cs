using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class Blog
    {
         public int Id { get; set; }

        [Required , MaxLength(100)]
        public string? Blog_Title { get; set; }
        [Required]
        public string? Blog_Content { get; set; }

        [Column(TypeName = "date")]
        public DateTime Blog_Date { get; set; }
        public string? Blog_Image { get; set; }
        public virtual ICollection<BlogTag>? BlogTag { get; set; }
    }
}
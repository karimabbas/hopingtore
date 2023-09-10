using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        public string? Image1 { get; set; }
        [Required]
        public string? Image2 { get; set; }
        [Required]
        public string? Image3 { get; set; }

        [ForeignKey("Product")]
        public int? Product_Id { get; set; }
        public Product? Product { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class ProductTag
    {
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("Tag")]
        public int Tag_Id { get; set; }
        public Tag? Tag { get; set; }
    }
}
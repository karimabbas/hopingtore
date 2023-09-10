using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopingStore.Models
{
    public class UserWishList
    {
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        [Required]
        public string? User_id { get; set; }
        public IdentityUser? User { get; set; }

        [ForeignKey("Product")]
        public int Product_id { get; set; }
        public Product? Product { get; set; }

    }
}
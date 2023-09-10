using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class Shipping
    {
        public int Id { get; set; }

        [Required, EmailAddress, Display(Name = "Email")]
        public string? Shipper_Email { get; set; }
        [Required, Display(Name = "Name")]
        public string? Shipper_Name { get; set; }
        public string? Address { get; set; }
        public int? Postal_Code { get; set; }
        public long Phone { get; set; }
        public string? Notes { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
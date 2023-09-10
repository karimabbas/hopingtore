using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ShopingStore.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("IdentityUser")]
        [Required]
        public string? Customer_Id { get; set; }
        public IdentityUser? IdentityUser { get; set; }
        public decimal Order_Total { get; set; }
        public int Order_Status { get; set; }

        [ForeignKey("Payment")]
        public int? Payment_Id { get; set; }
        public Payment? Payment { get; set; }
        [ForeignKey("Shipping")]
        public int? Shipping_Id { get; set; }
        public Shipping? Shipping { get; set; }
        public List<OrderDetails> OrderDetails {get;set;}

    }
}
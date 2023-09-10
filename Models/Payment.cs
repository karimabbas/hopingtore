using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public enum PaymentMethod
    {
        HandCash = 0,
        Paypal = 1,
    }
    public class Payment
    {
        public int Id { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
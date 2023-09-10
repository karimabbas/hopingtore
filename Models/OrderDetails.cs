using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int Order_Id { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public Product Product { get; set; }
        public int Product_Quantity { get; set; }
        public Color Product_Color { get; set; }
        public Size? Product_Size { get; set; }
        public decimal Total_price { get; set; }

    }
}
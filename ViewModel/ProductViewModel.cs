using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ShopingStore.Models;

namespace ShopingStore.ViewModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Popularity { get; set; }
        public int Stored_Quantity { get; set; }
        public Size Product_Size { get; set; }
        public Color Product_Color { get; set; }
        public DateTime Adding_date { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Product_Price { get; set; }

        [ForeignKey("Category")]
        public int Category_Id { get; set; }
        public Category? Category { get; set; }

        [ForeignKey("Image")]
        public int Image_Id { get; set; }
        public Image? Image { get; set; }

        public List<IFormFile>? Formfile { get; set; }

    public string? image1 { get; set; }
        public string? image2 { get; set; }
        public string? image3 { get; set; }
    }
}
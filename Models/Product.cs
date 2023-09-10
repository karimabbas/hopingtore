using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopingStore.Models
{
    public enum Size
    {
        size_XL = 0,
        size_L = 1,
        size_S = 2,
        size_M = 4
    }
    public enum Color
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        Yellow = 4,
        Black = 8,
        White = 16
    }
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Popularity { get; set; }
        public int Stored_Quantity { get; set; }
        public Size? Product_Size { get; set; }
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
        public ICollection<ProductTag>? ProductTags { get; set; }

    }
}
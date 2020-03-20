using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laborator5.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SKU { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }
        
        public static Product Create(int sku, string description, decimal price, string imageURL)
        {
            return new Product
            {
                SKU = sku,
                Description = description,
                Price = price,
                ImageURL = imageURL
            };
        }
    }
}

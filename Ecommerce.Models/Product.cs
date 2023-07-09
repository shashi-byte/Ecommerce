using NPoco;

namespace Ecommerce.Models
{
    [TableName("ProductDetails")]
    public class Product
    {
        [Column("ProductCode")]
        public string? Code { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("Price")]
        public double Price { get; set; }
        public int Quantity { get; set; } = 1;
        public double ActualPrice { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;

namespace web_rest_hudz_kp21.Models
{
    public class BikePart
    {
        public int PartId { get; set; }
        public string? PartType { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }

        public BikePart() { }

        public BikePart(
            int partId,
            string partType,
            string material,
            string manufacturer,
            float price,
            int stockQuantity
        )
        {
            PartId = partId;
            PartType = partType;
            Description = material;
            Manufacturer = manufacturer;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}

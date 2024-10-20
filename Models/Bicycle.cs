using System.ComponentModel.DataAnnotations;

namespace web_rest_hudz_kp21.Models
{
    public class Bicycle
    {
        public int BicycleId { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
        public double Weight { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }

        public Bicycle() { }

        public Bicycle(
            int bicycleId,
            string model,
            string type,
            string manufacturer,
            int year,
            double weight,
            float price,
            int stockQuantity
        )
        {
            BicycleId = bicycleId;
            Model = model;
            Type = type;
            Manufacturer = manufacturer;
            ReleaseYear = year;
            Weight = weight;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}




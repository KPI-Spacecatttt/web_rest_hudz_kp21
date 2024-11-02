using System.ComponentModel.DataAnnotations;

namespace web_rest_hudz_kp21.Models
{
    public class Bicycle
    {
        [Key]
        public int Id { get; set; }
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace web_rest_hudz_kp21.Models
{
    public class BikePart
    {
        [Key]
        public int Id { get; set; }
        public string? PartType { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
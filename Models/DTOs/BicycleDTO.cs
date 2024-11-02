namespace web_rest_hudz_kp21.Models.DTOs
{
    public class BicycleDTO
    {
        public string? Model { get; set; }
        public string? Type { get; set; }
        public string? Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
        public float Weight { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
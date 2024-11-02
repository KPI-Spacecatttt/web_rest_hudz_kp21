namespace web_rest_hudz_kp21.Models.DTOs
{
    public class BicycleSummaryDTO
    {
        public string? Model { get; set; }
        public string? Manufacturer { get; set; }
        public int ReleaseYear { get; set; }
        public int StockQuantity { get; set; }
    }
}
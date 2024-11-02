namespace web_rest_hudz_kp21.Models.DTOs
{
    public class BikePartSummaryDTO
    {
        public int Id { get; set; }
        public string? PartType { get; set; }
        public string? Manufacturer { get; set; }
        public int StockQuantity { get; set; }
    }
}
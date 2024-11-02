namespace web_rest_hudz_kp21.Models.DTOs
{
    public class BikePartDTO
    {
        public string? PartType { get; set; }
        public string? Description { get; set; }
        public string? Manufacturer { get; set; }
        public float Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
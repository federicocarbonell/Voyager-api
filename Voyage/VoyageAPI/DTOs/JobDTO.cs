namespace VoyageAPI.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public ProductDTO Product { get; set; }
        public State State { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public string Direction { get; set; }
    }
}
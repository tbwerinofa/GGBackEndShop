namespace ProductWebAPI.Models
{
    public class SaveResult
    {
        public int Id { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}

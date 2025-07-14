namespace Global.Models
{
    public class GeneralResponse
    {
        public GeneralResponse() {
            Message = "unknows";
            Status = "warning";
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public object? Data { get; set; }
    }
}

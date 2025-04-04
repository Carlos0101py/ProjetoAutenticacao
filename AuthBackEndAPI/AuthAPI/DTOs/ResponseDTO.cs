namespace AuthAPI.DTOs
{
    public class ResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object ?Date { get; set; }
    }
}
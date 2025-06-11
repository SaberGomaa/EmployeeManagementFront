namespace AribMVC.DTOs
{
    public class GResponse<T> where T : class
    {
        public T Data { get; set; }
        public bool IsSucceeded { get; set; }
        public string Error { get; set; }
        public string StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}

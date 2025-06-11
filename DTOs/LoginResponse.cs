namespace AribMVC.DTOs
{
    public class LoginResponse
    {
        public int userId { get; set; }
        public string Name { get; set; }
        public string token { get; set; }
        public int expirytimeinminutes { get; set; }

    }
}

namespace Models.Entities
{
    public record User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int EmpId { get; set; }

    }
}

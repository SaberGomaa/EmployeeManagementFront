namespace Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public decimal Salary { get; set; }
        public string Image { get; set; }
        public string ManagerName { get; set; }  
        public int DepartmentId { get; set; }
        public int? ManagerId { get; set; }

    }
}

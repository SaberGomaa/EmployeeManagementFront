using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Navigation property for Employees
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string City { get; set; } = "";
    }
}

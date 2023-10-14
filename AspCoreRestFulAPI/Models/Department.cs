namespace AspCoreRestFulAPI.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? DepartmentName { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}

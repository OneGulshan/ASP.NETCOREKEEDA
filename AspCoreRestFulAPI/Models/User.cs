namespace AspCoreRestFulAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? Contact { get; set; }
        public int? DepartmentId { get; set; }//User Data is depended on Department Table here
        public Department? Department { get; set; }
    }
}

using System.ComponentModel;

namespace ASPCORE.Models
{
    public class Person
    {
        public int PersonId { get; set; }
        [DisplayName("Name")]
        public string PersonName { get; set; } = "";
        public int Age { get; set; }
        public string Password { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsActive { get; set; }
        public int Gender { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public int Country { get; set; }
    }
    public enum MaritalStatus
    {
        Single = 1,
        Married = 2,
        Divorced = 3,
        Widowed = 4
    }
}

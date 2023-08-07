using System.ComponentModel.DataAnnotations;

namespace ASPCORE.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string ProfileImage { get; set; } = "";
    }
}

namespace ASPCORE.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public CourseType Courses { get; set; }
    }

    public enum CourseType
    {
        PartTime, FullTime
    }
}

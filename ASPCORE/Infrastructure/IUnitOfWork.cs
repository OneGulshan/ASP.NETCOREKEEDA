namespace ASPCORE.Infrastructure
{
    public interface IUnitOfWork
    {
        IStudentRepo StudentRepo { get; }
        void Save();//Ye Function hamare Context ko save karne ke kaam aega
    }
}

using ASPCORE.Models;

namespace ASPCORE.GenericInfrastructure
{
    public interface IRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Delete(int id);
    }
}

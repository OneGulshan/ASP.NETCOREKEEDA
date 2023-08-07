using ASPCORE.Data;
using ASPCORE.GenericInfrastructure;
using Microsoft.EntityFrameworkCore;

namespace ASPCORE.GenericRepository
{
    public class Repository<T> : IRepo<T> where T : class
    {
        private readonly DataContext _context;
        private readonly DbSet<T> _entities;//Here in DbSet T is our generic class

        public Repository(DataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();//Here we set our all queries in Generic DbSet using Generic Initialization Set. isse ab ham apni sarri entities _entities ke throw access kar sakte hain.
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();//Here Access our all Models/Entities one by one
        }

        public T GetById(int id)
        {
            return _entities.Find(id);
        }

        public T Delete(int id)
        {
            var result = _entities.Find(id);
            _entities.Remove(result);
            _context.SaveChanges();
            return result;
        }
    }
}

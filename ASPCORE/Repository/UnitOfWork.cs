using ASPCORE.Data;
using ASPCORE.Infrastructure;

namespace ASPCORE.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IStudentRepo _repo;

        public UnitOfWork(DataContext context, IStudentRepo repo)
        {
            _context = context;
            _repo = repo;
        }

        public IStudentRepo StudentRepo
        {
            get
            {
                return _repo ??= new StudentRepo(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

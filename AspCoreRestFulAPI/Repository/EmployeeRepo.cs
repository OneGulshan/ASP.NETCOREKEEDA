using AspCoreRestFulAPI.Data;
using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace AspCoreRestFulAPI.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DataContext _context;

        public EmployeeRepo(DataContext context)
        {
            _context = context;
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            var result = await _context.Employee.AddAsync(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employee.ToListAsync();
        }

        public async Task<Employee?> GetEmployee(int id)
        {
            return await _context.Employee.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<Employee?> UpdateEmployee(Employee employee)
        {
            var result = await _context.Employee.FirstOrDefaultAsync(_ => _.Id == employee.Id);
            if (result != null)
            {
                result.Name = employee.Name;
                result.City = employee.City;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Employee?> DeleteEmployee(int id)//Now here returning deleted employee also.
        {
            var result = await _context.Employee.Where(_ => _.Id == id).FirstOrDefaultAsync();
            if (result != null)
            {
                _context.Employee.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<Employee?>> SearchName(string? value)
        {
            IQueryable<Employee> query = _context.Employee;//here using IQueryable interface we converting our Employee context table in IQueryable type query for quering/searching in Employee table using its Contains method.

            if (!string.IsNullOrEmpty(value))//If name is not null or Empty then perfom search operation
            {
                query = query.Where(_ => _.Name.Contains(value)).Concat(query.Where(_ => _.City.Contains(value)));
            }
            return await query.ToListAsync();
        }
    }
}

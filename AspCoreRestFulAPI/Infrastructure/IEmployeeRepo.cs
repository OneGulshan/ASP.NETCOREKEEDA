using DataAccessLayer;

namespace AspCoreRestFulAPI.Infrastructure
{
    public interface IEmployeeRepo
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee?> GetEmployee(int id);
        Task<Employee> AddEmployee(Employee employee);
        Task<Employee?> UpdateEmployee(Employee employee);
        Task<Employee?> DeleteEmployee(int id);
        Task<IEnumerable<Employee?>> SearchName(string ?value);
    }
}

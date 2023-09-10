using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Command
{
    public class DeleteEmployeeCommand : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}

using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data
{
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}

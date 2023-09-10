using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Handlers
{
    public class GetEmployeeListHandlers : IRequestHandler<GetEmployeeListQuery, List<Employee>>
    {
        private readonly IEmployeeRepo _repo;

        public GetEmployeeListHandlers(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        public async Task<List<Employee>> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            return (List<Employee>)await _repo.GetEmployees();
        }
    }
}

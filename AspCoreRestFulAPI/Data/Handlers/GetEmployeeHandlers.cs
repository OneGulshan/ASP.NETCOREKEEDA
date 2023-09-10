using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Handlers
{
    public class GetEmployeeHandlers : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeRepo _repo;

        public GetEmployeeHandlers(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _repo.GetEmployee(request.Id);
        }
    }
}

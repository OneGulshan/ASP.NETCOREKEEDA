using AspCoreRestFulAPI.Data.Command;
using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Handlers
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Employee>//This Employee is our return type
    {
        private readonly IEmployeeRepo _repo;

        public CreateEmployeeHandler(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        public async Task<Employee> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            //return await _repo.AddEmployee(request.)
            Employee emp = new()
            {
                Name = request.Name,
                City = request.City
            };
            return await _repo.AddEmployee(emp);
        }
    }
}

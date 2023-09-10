using AspCoreRestFulAPI.Data.Command;
using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepo _repo;

        public UpdateEmployeeHandler(IEmployeeRepo repo)
        {
            _repo = repo;
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<Employee?> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repo.GetEmployee(request.Id);
            if (employee == null) return default;

            employee.Name = request.Name;
            employee.City = request.City;
            return await _repo.UpdateEmployee(employee);
        }
    }
}

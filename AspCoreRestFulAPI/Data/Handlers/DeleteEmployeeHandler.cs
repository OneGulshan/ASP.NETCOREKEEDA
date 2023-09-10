using AspCoreRestFulAPI.Data.Command;
using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Handlers
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, Employee>
    {
        private readonly IEmployeeRepo _repo;

        public DeleteEmployeeHandler(IEmployeeRepo repo)
        {
            _repo = repo;
        }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
        public async Task<Employee?> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _repo.GetEmployee(request.Id);
            if (employee == null) return default;
            return await _repo.DeleteEmployee(request.Id);
        }
    }
}

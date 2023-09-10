using AspCoreRestFulAPI.Data;
using AspCoreRestFulAPI.Data.Command;
using DataAccessLayer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreRestFulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Employee>> EmployeeList()
        {
            var employeeList = await _mediator.Send(new GetEmployeeListQuery());
            return employeeList;
        }

        [HttpGet("{id:int}")]
        public async Task<Employee> EmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery() { Id = id });
            return employee;
        }

        [HttpPost]
        public async Task<Employee> AddEmployee(Employee employee)
        {
            var employeeReturn = await _mediator.Send(new CreateEmployeeCommand(employee.Name, employee.City));
            return employeeReturn;
        }

        [HttpPut("{id:int}")]
        public async Task<Employee?> UpdateEmployee(Employee employee)
        {
            var employeeReturn = await _mediator.Send(new UpdateEmployeeCommand(employee.Id, employee.Name, employee.City));
            return employeeReturn;
        }

        [HttpDelete("{id:int}")]
        public async Task<Employee?> DeleteEmployee(int id)
        {
            return await _mediator.Send(new DeleteEmployeeCommand() { Id = id });
        }
    }
}

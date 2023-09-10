using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Command
{
    public class CreateEmployeeCommand : IRequest<Employee>
    {
        public CreateEmployeeCommand(string name, string city) 
        {
            Name = name;
            City = city;
        }
        public string Name { get; set; }
        public string City { get; set; }
    }
}

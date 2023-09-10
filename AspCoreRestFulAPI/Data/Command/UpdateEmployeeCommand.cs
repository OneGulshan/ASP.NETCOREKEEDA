using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data.Command
{
    public class UpdateEmployeeCommand : IRequest<Employee>
    {
        public UpdateEmployeeCommand(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
        public int Id { get; set; }
        public string  Name { get; set; }
        public string  City { get; set; }
    }
}

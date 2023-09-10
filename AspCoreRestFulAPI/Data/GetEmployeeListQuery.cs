using DataAccessLayer;
using MediatR;

namespace AspCoreRestFulAPI.Data
{
    public class GetEmployeeListQuery :IRequest<List<Employee>>
    {
    }
}

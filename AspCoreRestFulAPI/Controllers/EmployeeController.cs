using AspCoreRestFulAPI.Filters;
using AspCoreRestFulAPI.Infrastructure;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreRestFulAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//401Unauthorized Shows here
    //[Example("Controller Level")]//Here Used our Custom Filter as a Controller Level, We Can use as a Action Level also
    [ExampleAsyncFilter("Controller Level")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _repo;
        public EmployeeController(IEmployeeRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await _repo.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data form Database");
            }
        }
        //[Example("Controller Level")]
        [ExampleAsyncFilter("Action Level", -1)]
        [HttpGet("{id:int}")]//Here seprating our Http Verbs using extra id parameter for saving from multimaching requests and conflictions, when url hitting to api for requests.
        public async Task<ActionResult<Employee>> GetEmployee(int id)//Yahan DataAccessLayer me hamare Model Employee ko ActionResult me compiler explicitly convert nahi kar pa rha tha jise hamne implicitly convert kar liya hai
        {
            try
            {
                var result = await _repo.GetEmployee(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data form Database");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            try
            {
                if (employee == null)
                {
                    return BadRequest();//If employee found null then sent BadRequest
                }
                var CreatedEmployee = await _repo.AddEmployee(employee);
                return CreatedAtAction(nameof(GetEmployee), new { id = CreatedEmployee.Id }, CreatedEmployee);//here returning our CreatedEmployee using CreatedAtAction method with desired location where employee is created, In CreatedAtAction method we required to pass action method name, id and created employee also.
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data form Database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee?>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Id Mistmach");
                }
                var employeeUpdate = await _repo.GetEmployee(id);//Here employee retrieving for showing error message only
                if (employeeUpdate == null)
                {
                    return NotFound($"Employee Id={id} not Found");
                }
                return await _repo.UpdateEmployee(employee);//Here Updated employee directly, because in repository updated code already defined
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data form Database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee?>> DeleteEmployee(int id)
        {
            try
            {
                var employeeDelete = await _repo.GetEmployee(id);
                if (employeeDelete == null)
                {
                    return NotFound($"Employee Id={id} not Found");
                }
                return await _repo.DeleteEmployee(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data form Database");
            }
        }

        [HttpGet("{search}")]//Here performing attribute routing
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string value)//IEnumerable adding for Getting Multiple Employees here
        {
            try
            {
                var result = await _repo.SearchName(value);
                if (result.Any())//here result me kuch bhi aaega to Ok status message ke sath result show ho jaega.
                {
                    return Ok(result);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error in Retrieving Data form Database");
            }
        }
    }
}

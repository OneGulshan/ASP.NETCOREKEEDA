using AspCoreKeedaRestFulAPIConsuming.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AspCoreKeedaRestFulAPIConsuming.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://localhost:13912/")
            };
            var response = await client.PostAsJsonAsync("api/employees", employee);//we don't know witch type of data we recieved so use var type variable here
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> employees = new();
            HttpClient client = new()//this client is for connecting with our web api
            {
                BaseAddress = new Uri("http://localhost:13912/")//here define your API App url here/Base Address <- by selecting Debug properties -> Url
            };
            HttpResponseMessage response = await client.GetAsync("api/employees");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;//here converting our response Content into string result by reading.                
                employees = JsonConvert.DeserializeObject<List<Employee>>(results) ?? throw new InvalidOperationException();//Now this results convert into Employee List/Action Result Form using JsonConvert DeserializeObject method by installing Newtonsoft.Json namespace                
            }
            return View(employees);
        }

        public async Task<IActionResult> Details(int id)
        {
            Employee employee = await GetEmployeeByID(id);//Here Getting Single Employee Details so, we extract it and reusing in Edit action method also
            return View(employee);
        }

        private static async Task<Employee> GetEmployeeByID(int id)
        {
            Employee employee = new();
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://localhost:13912/")
            };
            HttpResponseMessage response = await client.GetAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                var results = response.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<Employee>(results) ?? throw new InvalidOperationException("Http Client 'Employee' list not found");
            }
            return employee;
        }

        public async Task<IActionResult> Edit(int id)
        {
            Employee employee = await GetEmployeeByID(id);
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://localhost:13912/")
            };
            var response = await client.PutAsJsonAsync($"api/employees/{employee.Id}", employee);//Here Same Code Copy Paste of Create Post Action Method and only changed HttpClient method here by PutAsJsonAsync
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpClient client = new()
            {
                BaseAddress = new Uri("http://localhost:13912/")
            };
            var response = await client.DeleteAsync($"api/employees/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}

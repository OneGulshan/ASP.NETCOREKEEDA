using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspCoreRestFulAPI.Filters
{
    public class ExampleAttribute : Attribute, IActionFilter
    {
        private readonly string _name;

        public ExampleAttribute(string name)
        {
            _name = name;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine($"Action Method Executed with {_name}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine($"Action Method Executing with {_name}");
        }
    }
}

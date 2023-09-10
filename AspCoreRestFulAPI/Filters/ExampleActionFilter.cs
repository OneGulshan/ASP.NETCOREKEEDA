using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspCoreRestFulAPI.Filters
{
    public class ExampleActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)//This method means phele
        {
            Debug.WriteLine("Before Action");
        }

        public void OnActionExecuted(ActionExecutedContext context)//This method means baadme
        {
            Debug.WriteLine("After Action");
        }
    }
}

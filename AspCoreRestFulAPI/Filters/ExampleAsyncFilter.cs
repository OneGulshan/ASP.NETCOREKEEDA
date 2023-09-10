using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace AspCoreRestFulAPI.Filters
{
    public class ExampleAsyncFilter : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        private string _actionName;

        public ExampleAsyncFilter(string actionName, int order=0)
        {
            _actionName = actionName;
            Order = order;
        }

        public int Order { get; set; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)//Here We have Found Delegate of Next, which used for executing next proccess of pipeline
        {
            Debug.WriteLine($"Before - {_actionName}");
            await next();
            Debug.WriteLine($"After - {_actionName}");
        }
    }
}

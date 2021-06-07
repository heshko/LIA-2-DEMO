using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using salesNowBackend.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.ActionFilter
{
    public class ValidationActivityExistsAttribute : IAsyncActionFilter
    {
        private readonly IActivityFirestore _activityFirestore;
        public ValidationActivityExistsAttribute(IActivityFirestore activityFirestore)
        {
            _activityFirestore = activityFirestore;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var id = context.ActionArguments["activityId"].ToString();
            var activity = _activityFirestore.GetActivityById("Activities", id);
            if (activity is null)
            {
                context.Result = new NotFoundObjectResult($"Activity with Id {id} not Exists in Database");
                return;
            }

            await next();
        }
    }
}

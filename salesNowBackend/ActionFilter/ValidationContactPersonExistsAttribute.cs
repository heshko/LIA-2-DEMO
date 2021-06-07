using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using salesNowBackend.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.ActionFilter
{
    public class ValidationContactPersonExistsAttribute : IAsyncActionFilter
    {

        private readonly IContactPersonFirestore _contactPersonFirestore;
        public ValidationContactPersonExistsAttribute(IContactPersonFirestore contactPersonFirestore)
        {
            _contactPersonFirestore = contactPersonFirestore;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = context.ActionArguments["contactPersonId"].ToString();
            var contactPerson = _contactPersonFirestore.GetContactPersonById("ContactPersons", id);
            if (contactPerson is null)
            {
                context.Result = new NotFoundObjectResult($"ContactPerson with Id {id} not Exists in Database");
                return;
            }

            await next();
        }
    }
}

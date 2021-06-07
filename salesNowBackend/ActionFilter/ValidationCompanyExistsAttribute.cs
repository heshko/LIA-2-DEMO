using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using salesNowBackend.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.ActionFilter
{
    public class ValidationCompanyExistsAttribute : IAsyncActionFilter
    {
        private readonly ICompanyFirestore _companyFirestore;
        public ValidationCompanyExistsAttribute(ICompanyFirestore companyFirestore)
        {
            _companyFirestore = companyFirestore;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = context.ActionArguments["companyId"].ToString();
            var company = await _companyFirestore.GetCompaniesById("Companies",id);
            if (company is null)
            {
                context.Result = new NotFoundObjectResult($" Company with id {id} not existes in Database");
                return;
            }
          
            await next();
        }
    }
}

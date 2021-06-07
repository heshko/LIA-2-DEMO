using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using salesNowBackend.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace salesNowBackend.ActionFilter
{
    public class ValidationBusinessOpportunityExistsAttribute : IAsyncActionFilter
    {
        private readonly IBusinessOpportunityFirestore _businessOpportunityFirestore;
        public ValidationBusinessOpportunityExistsAttribute(IBusinessOpportunityFirestore businessOpportunityFirestore)
        {
            _businessOpportunityFirestore = businessOpportunityFirestore;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = context.ActionArguments["businessOpportunityId"].ToString();
            var businessOpportunity = await _businessOpportunityFirestore.GetBusinessOpportunityById("BusinessOpportunities", id);
            if (businessOpportunity is null)
            {
                context.Result = new NotFoundObjectResult($" BusinessOpportunities with id {id} not existes in Database");
                return;
            }

            await next();
        }
    }
}

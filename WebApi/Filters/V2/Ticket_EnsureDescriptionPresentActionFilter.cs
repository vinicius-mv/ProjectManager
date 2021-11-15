using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Filters.V2
{
    // As the logic validation must be applied only in a specific version, an Action Filter is suited for the issue,
    // Validation logic attributes could not be applied directly in the model because it would force every version of the Api apply the validation 
    // so we can mark class or actions that must be applied this validation
    public class Ticket_EnsureDescriptionPresentActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ticket = context.ActionArguments["ticket"] as Ticket;

            if(ticket != null && !ticket.ValidateDescription())
            {
                context.ModelState.AddModelError("Description", "Description is required");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}

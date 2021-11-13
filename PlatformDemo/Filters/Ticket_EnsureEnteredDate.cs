using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Filters
{
    public class Ticket_EnsureEnteredDate : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ticket = context.ActionArguments["ticket"] as Ticket;

            if (ticket != null && 
                !string.IsNullOrEmpty(ticket.Owner) && 
                ticket.EnteredDate.HasValue == false)
            {
                context.ModelState.AddModelError($"{nameof(ticket.EnteredDate)}", $"{nameof(ticket.EnteredDate)} is required");
                // short circuit MVC pipeline
                context.Result = new BadRequestObjectResult(context.ModelState);
            }


        }
    }
}

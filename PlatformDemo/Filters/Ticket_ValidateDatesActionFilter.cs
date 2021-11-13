using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Filters
{
    public class Ticket_ValidateDatesActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ticket = context.ActionArguments["ticket"] as Ticket;

            if (ticket != null &&
                !string.IsNullOrEmpty(ticket.Owner))
            {
                bool isValid = true;

                if (ticket.EnteredDate.HasValue == false)
                {
                    context.ModelState.AddModelError($"EnteredDate", $"EnteredDate is required.");
                    isValid = false;


                }

                if (ticket.EnteredDate.HasValue &&
                    ticket.DueDate.HasValue &&
                    ticket.EnteredDate > ticket.DueDate)
                {
                    context.ModelState.AddModelError("DueDate", "DueDate has to be later than the EnteredDate.");
                    isValid = false;
                }

                if (!isValid)
                {
                    //var problemDetails = new ValidationProblemDetails(context.ModelState)
                    //{
                    //    Status = StatusCodes.Status400BadRequest
                    //};
                    //context.Result = new BadRequestObjectResult(problemDetails);
                    context.Result = new BadRequestObjectResult(context.ModelState);
                }

            }
        }
    }
}

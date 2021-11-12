using PlatformDemo.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.ModelValidations
{
    public class Ticket_EnsureDueDateForTicketOwner : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var ticket = (Ticket)validationContext.ObjectInstance;

            if (!string.IsNullOrWhiteSpace(ticket.Owner))
            {
                if (!ticket.DueDate.HasValue)
                    return new ValidationResult("Due date is required when ticket has an owner");
            }

            return ValidationResult.Success;
        }
    }
}

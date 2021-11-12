using Microsoft.AspNetCore.Mvc;
using PlatformDemo.ModelValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformDemo.Models
{
    public class Ticket
    {
        public int? TicketId { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        [Ticket_EnsureDueDateForTicketOwner]
        [Ticket_EnsureDueDateInFuture]
        public DateTime? DueDate { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
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

        [EmailAddress]
        public string Email { get; set; }

        [Range(0, 10_000)]
        public double Price { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [MaxLength(64_000)]
        public byte[] File { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [Phone]
        public string  PhoneNumber { get; set; }
    }
}

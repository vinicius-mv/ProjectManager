﻿using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketScreenUseCases
    {
        Task<IEnumerable<Ticket>> ViewTickets(int projectId);
    }
}
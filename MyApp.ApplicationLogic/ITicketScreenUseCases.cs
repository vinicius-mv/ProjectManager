﻿using Core.Models;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketScreenUseCases
    {
        Task<int> AddTicketAsync(Ticket ticket);
        Task DeleteTicketAsync(int ticketId);
        Task UpdateTicketAsync(Ticket ticket);
        Task<Ticket> ViewTicketByIdAsync(int ticketId);
    }
}
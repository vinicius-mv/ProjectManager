﻿using Core.Models;
using MyApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketsScreenUseCases : ITicketsScreenUseCases
    {
        private readonly IProjectRepository projectRepository;
        private readonly ITicketRepository ticketRepository;

        public TicketsScreenUseCases(IProjectRepository projectRepository, ITicketRepository ticketRepository)
        {
            this.projectRepository = projectRepository;
            this.ticketRepository = ticketRepository;
        }

        public async Task<IEnumerable<Ticket>> ViewTicketsAsync(int projectId)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId);
        }

        public async Task<IEnumerable<Ticket>> SearchTicketsAsync(string filter)
        {
            if (int.TryParse(filter, out int ticketId))
            {
                var ticket = await ticketRepository.GetByIdAsync(ticketId);
                var tickets = new List<Ticket>() { ticket };
                return tickets;
            }
            return await ticketRepository.GetAsync(filter);
        }

        public async Task<IEnumerable<Ticket>> ViewOwnerTicketsAsync(int projectId, string ownerName)
        {
            return await projectRepository.GetProjectTicketsAsync(projectId, ownerName);
        }
    }
}

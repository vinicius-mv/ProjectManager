using Core.Models;
using MyApp.Repository;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public class TicketScreenUseCases : ITicketScreenUseCases
    {
        private readonly ITicketRepository ticketRepository;

        public TicketScreenUseCases(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public async Task<int> AddTicketAsync(Ticket ticket)
        {
            return await ticketRepository.CreateAsync(ticket);
        }

        public async Task<Ticket> ViewTicketByIdAsync(int ticketId)
        {
            return await ticketRepository.GetByIdAsync(ticketId);
        }

        public async Task UpdateTicketAsync(Ticket ticket)
        {
            await ticketRepository.UpdateAsync(ticket);
        }

        public async Task DeleteTicketAsync(int ticketId)
        {
            await ticketRepository.DeleteAsync(ticketId);
        }
    }
}

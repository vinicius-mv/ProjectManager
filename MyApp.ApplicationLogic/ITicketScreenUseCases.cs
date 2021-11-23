using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketScreenUseCases
    {
        Task<IEnumerable<Ticket>> ViewTickets(int projectId);
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task<IEnumerable<Ticket>> ViewOwnerTickets(int projectId, string ownerName);
        Task<Ticket> ViewTicketById(int ticketId);
        Task UpdateTicket(Ticket ticket);
    }
}
using Core.Models;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketScreenUseCases
    {
        Task<int> AddTicket(Ticket ticket);
        Task UpdateTicket(Ticket ticket);
        Task<Ticket> ViewTicketById(int ticketId);
    }
}
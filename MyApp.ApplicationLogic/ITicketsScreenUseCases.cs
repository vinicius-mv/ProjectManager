using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<IEnumerable<Ticket>> SearchTickets(string filter);
        Task<IEnumerable<Ticket>> ViewOwnerTickets(int projectId, string ownerName);
        Task<IEnumerable<Ticket>> ViewTickets(int projectId);
    }
}
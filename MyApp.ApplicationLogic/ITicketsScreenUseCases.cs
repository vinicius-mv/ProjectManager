using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    public interface ITicketsScreenUseCases
    {
        Task<IEnumerable<Ticket>> SearchTicketsAsync(string filter);
        Task<IEnumerable<Ticket>> ViewOwnerTicketsAsync(int projectId, string ownerName);
        Task<IEnumerable<Ticket>> ViewTicketsAsync(int projectId);
    }
}
using MyApp.Repository;
using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;


public class Program
{
    public static async Task Main(string[] args)
    {

        HttpClient httpClient = new HttpClient();
        IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);

        await TestProjects();

        Console.WriteLine("\n\n");

        await TestTickets();

        #region Projects

        async Task TestProjects()
        {
            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Reading projects...");
            await GetProjects();

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Reading project tickets...");
            await GetProjectTickets(1);

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Creating a Project...");
            var projectId = await CreateProject();
            await GetProjects();

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Updating a Project...");
            var project = await GetProject(projectId);
            await UpdateProject(project);
            await GetProjects();

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Deleting a Project...");
            await DeleteProject(projectId);
            await GetProjects();
        }

        async Task GetProjects()
        {
            ProjectRepository repository = new ProjectRepository(apiExecuter);

            var projects = await repository.GetAsync();
            foreach (var project in projects)
            {
                Console.WriteLine($"Project: {project.Name}");
            }
        }

        async Task<Project> GetProject(int id)
        {
            ProjectRepository repository = new ProjectRepository(apiExecuter);
            return await repository.GetByIdAsync(id);
        }

        async Task GetProjectTickets(int id)
        {
            var project = await GetProject(id);
            Console.WriteLine($"Project: {project.Name}");

            var repository = new ProjectRepository(apiExecuter);
            var tickets = await repository.GetProjectTicketsAsync(id);
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"    Ticket: {ticket.Title}");
            }
        }

        async Task<int> CreateProject()
        {
            var project = new Project { Name = "Another project" };
            ProjectRepository repository = new ProjectRepository(apiExecuter);
            return await repository.CreateAsync(project);
        }

        async Task UpdateProject(Project project)
        {
            ProjectRepository repository = new ProjectRepository(apiExecuter);
            project.Name = $"Project {project.ProjectId} updated";
            await repository.UpdateAsync(project);
        }

        async Task DeleteProject(int projectId)
        {
            ProjectRepository repository = new ProjectRepository(apiExecuter);
            await repository.DeleteAsync(projectId);
        }
        #endregion

        #region Tickets

        async Task TestTickets()
        {
            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Reading tickets...");
            await GetTickets();

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Reading tickets contains 1...");
            await GetTickets("1");

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Creating a Ticket...");
            var ticketId = await CreateTicket();
            await GetTickets();

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Updating a Ticket...");
            var ticket = await GetTicketById(ticketId);
            await UpdateTicket(ticket);
            await GetTickets();

            Console.WriteLine("///////////////////////////");
            Console.WriteLine("Deleting a Ticket...");
            await DeleteTicket(ticketId);
            await GetTickets();
        }

        async Task GetTickets(string filter = null)
        {
            TicketRepository ticketRepository = new TicketRepository(apiExecuter);
            var tickets = await ticketRepository.GetAsync(filter);

            foreach (var ticket in tickets)
            {
                Console.WriteLine($"Ticket: {ticket.Title}");
            }
        }

        async Task<Ticket> GetById(int id)
        {
            TicketRepository ticketRepository = new TicketRepository(apiExecuter);
            var ticket = await ticketRepository.GetByIdAsync(id);
            return ticket;
        }

        async Task<Ticket> GetTicketById(int id)
        {
            TicketRepository ticketRepository = new TicketRepository(apiExecuter);
            var ticket = await ticketRepository.GetByIdAsync(id);
            return ticket;
        }

        async Task<int> CreateTicket()
        {
            TicketRepository ticketRepository = new TicketRepository(apiExecuter);
            return await ticketRepository.CreateAsync(
                new Ticket
                {
                    ProjectId = 2,
                    Title = "New Ticket",
                    Description = "Somethigng is wrong on the server"
                });
        }

        async Task UpdateTicket(Ticket ticket)
        {
            TicketRepository ticketRepository = new TicketRepository(apiExecuter);
            ticket.Title += " Updated";
            await ticketRepository.UpdateAsync(ticket);
        }

        async Task DeleteTicket(int id)
        {
            TicketRepository ticketRepository = new TicketRepository(apiExecuter);
            await ticketRepository.DeleteAsync(id);
        }


        #endregion
    }
}

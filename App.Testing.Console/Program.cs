using App.Repository;
using App.Repository.ApiClient;
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

        Console.WriteLine("///////////////////////////");
        Console.WriteLine("Reading projects...");
        await GetProjects();

        Console.WriteLine("///////////////////////////");
        Console.WriteLine("Reading project tickets...");
        await GetProjectTickets(1);

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
    }
}

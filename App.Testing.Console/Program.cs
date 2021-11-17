using App.Repository;
using App.Repository.ApiClient;
using System;
using System.Net.Http;
using System.Threading.Tasks;


public class Program
{
    public static async Task Main(string[] args)
    {

        HttpClient httpClient = new HttpClient();
        IWebApiExecuter apiExecuter = new WebApiExecuter("https://localhost:5001", httpClient);

        await GetProjects();

        async Task GetProjects()
        {
            ProjectRepository repository = new ProjectRepository(apiExecuter);

            var projects = await repository.Get();
            foreach (var project in projects)
            {
                Console.WriteLine($"Project: {project.Name}");
            }
        }
    }
}

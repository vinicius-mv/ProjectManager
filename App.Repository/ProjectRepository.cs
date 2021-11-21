using App.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repository
{
    public class ProjectRepository
    {
        private readonly IWebApiExecuter _webApiExecuter;

        public ProjectRepository(IWebApiExecuter webApiExecuter)
        {
            _webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Project>> GetAsync()
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Project>>("api/projects");
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _webApiExecuter.InvokeGet<Project>($"api/projects/{id}");
        }

        public async Task<IEnumerable<Ticket>> GetProjectTicketsAsync(int projectId)
        {
            return await _webApiExecuter.InvokeGet<IEnumerable<Ticket>>($"api/projects/{projectId}/tickets");
        }

        public async Task<int> CreateAsync(Project project)
        {
            project = await _webApiExecuter.InvokePost("api/projects", project);
            return project.ProjectId;
        }

        public async Task UpdateAsync(Project project)
        {
            await _webApiExecuter.InvokePut($"api/projects/{project.ProjectId}", project);
        }

        public async Task DeleteAsync(int projectId)
        {
            await _webApiExecuter.InvokeDelete($"api/projects/{projectId}");
        }
    }
}

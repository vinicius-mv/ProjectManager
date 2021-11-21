using MyApp.Repository;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.ApplicationLogic
{
    // it's common to call this layer Service, so this class would call ProjectsService
    public class ProjectsScreenUseCases : IProjectsScreenUseCases
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsScreenUseCases(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjectsAsync()
        {
            return await _projectRepository.GetAsync();
        }
    }
}

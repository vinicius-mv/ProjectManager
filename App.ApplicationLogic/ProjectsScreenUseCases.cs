using App.Repository;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.ApplicationLogic
{
    // it's common to call this layer Service, so this class would call ProjectsService
    public class ProjectsScreenUseCases
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsScreenUseCases(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> ViewProjects()
        {
            return await _projectRepository.GetAsync();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IProjectInformationService : IServiceBase<ProjectInformation>
    {

    }
    public class ProjectInformationService : IProjectInformationService
    {
        private readonly IProjectInformationRepository repository;


        public ProjectInformationService(IProjectInformationRepository repository)
        {
            this.repository = repository;

        }
        public IEnumerable<ProjectInformation> GetAll()
        {
            var entities = repository.GetAll().Where(x => x.IsActive);
            return entities;
        }

        public void Save()
        {
            repository.Commit();
        }
        public ProjectInformation Create(ProjectInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public ProjectInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Update(ProjectInformation objectToUpdate)
        {
            repository.Update(objectToUpdate);
            Save();
        }

        public void Delete(int id)
        {
            var entity = repository.GetById(id);
            repository.Delete(entity);
            Save();
        }
    }
}

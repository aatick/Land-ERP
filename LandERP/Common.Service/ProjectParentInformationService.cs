using System.Collections.Generic;
using System.Linq;
using Common.Data.CommonDataModel;
using Common.Data.CommonRepository;

namespace Common.Service
{
    public interface IProjectParentInformationService : IServiceBase<ProjectParentInformation>
    {

    }
    public class ProjectParentInformationService : IProjectParentInformationService
    {
        private readonly IProjectParentInformationRepository repository;


        public ProjectParentInformationService(IProjectParentInformationRepository repository)
        {
            this.repository = repository;

        }
        public IEnumerable<ProjectParentInformation> GetAll()
        {
            var entities = repository.GetAll().Where(x => x.IsActive);
            return entities;
        }

        public void Save()
        {
            repository.Commit();
        }
        public ProjectParentInformation Create(ProjectParentInformation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public ProjectParentInformation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Update(ProjectParentInformation objectToUpdate)
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

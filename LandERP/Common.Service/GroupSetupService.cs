using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using Common.Data.CommonRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Service
{
    public interface IGroupSetupService : IServiceBase<GroupSetup>
    {

    }
    public class GroupSetupService : IGroupSetupService
    {
        private readonly IGroupSetupRepository repository;

        public GroupSetupService(IGroupSetupRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<GroupSetup> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public GroupSetup GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public GroupSetup Create(GroupSetup objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(GroupSetup objectToUpdate)
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

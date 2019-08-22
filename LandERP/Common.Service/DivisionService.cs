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
    public interface IDivisionService : IServiceBase<Division>
    {

    }
    public class DivisionService : IDivisionService
    {
        private readonly IDivisionRepository repository;

        public DivisionService(IDivisionRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Division> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Division GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Division Create(Division objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Division objectToUpdate)
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

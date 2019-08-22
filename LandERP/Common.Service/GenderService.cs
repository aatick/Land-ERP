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
    public interface IGenderService : IServiceBase<Gender>
    {

    }
    public class GenderService : IGenderService
    {
        private readonly IGenderRepository repository;

        public GenderService(IGenderRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Gender> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Gender GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Gender Create(Gender objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Gender objectToUpdate)
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

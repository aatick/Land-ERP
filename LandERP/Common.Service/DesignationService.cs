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
    public interface IDesignationService : IServiceBase<Designation>
    {

    }
    public class DesignationService : IDesignationService
    {
        private readonly IDesignationRepository repository;

        public DesignationService(IDesignationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Designation> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Designation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Designation Create(Designation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Designation objectToUpdate)
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

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
    public interface IDistrictService : IServiceBase<District>
    {

    }
    public class DistrictService : IDistrictService
    {
        private readonly IDistrictRepository repository;

        public DistrictService(IDistrictRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<District> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public District GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public District Create(District objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(District objectToUpdate)
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

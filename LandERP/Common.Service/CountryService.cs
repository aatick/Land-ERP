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
    public interface ICountryService : IServiceBase<Country>
    {

    }
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository repository;

        public CountryService(ICountryRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Country> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Country GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Country Create(Country objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Country objectToUpdate)
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

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
    public interface IThanaService : IServiceBase<Thana>
    {

    }
    public class ThanaService : IThanaService
    {
        private readonly IThanaRepository repository;

        public ThanaService(IThanaRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Thana> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Thana GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Thana Create(Thana objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Thana objectToUpdate)
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

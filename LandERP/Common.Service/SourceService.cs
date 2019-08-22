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
    public interface ISourceService : IServiceBase<Source>
    {
    }
    public class SourceService : ISourceService
    {
        private readonly ISourceRepository repository;

        public SourceService(ISourceRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Source> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Source GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Source Create(Source objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Source objectToUpdate)
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

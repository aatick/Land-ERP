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
    public interface IRelationService : IServiceBase<Relation>
    {

    }
    public class RelationService : IRelationService
    {
        private readonly IRelationRepository repository;

        public RelationService(IRelationRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Relation> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public Relation GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Relation Create(Relation objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Relation objectToUpdate)
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

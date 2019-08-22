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
    public interface IProfessionService : IServiceBase<Profession>
    {

    }
    public class ProfessionService : IProfessionService
    {
        private readonly IProfessionRepository repository;

        public ProfessionService(IProfessionRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Profession> GetAll()
        {
            var entities = repository.GetAll().Where(x=> x.IsActive).OrderBy(x=> x.ProfessionName);
            return entities;
        }
        public Profession GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Profession Create(Profession objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Profession objectToUpdate)
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

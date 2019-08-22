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
    public interface IBankService : IServiceBase<Bank>
    {
    }
    public class BankService : IBankService
    {
        private readonly IBankRepository repository;

        public BankService(IBankRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Bank> GetAll()
        {
            var entities = repository.GetAll().OrderBy(x => x.BankName);
            return entities;
        }
        public Bank GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public Bank Create(Bank objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(Bank objectToUpdate)
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

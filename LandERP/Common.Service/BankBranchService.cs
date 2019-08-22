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
    public interface IBankBranchService : IServiceBase<BankBranch>
    {
    }
    public class BankBranchService : IBankBranchService
    {
        private readonly IBankBranchRepository repository;

        public BankBranchService(IBankBranchRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<BankBranch> GetAll()
        {
            var entities = repository.GetAll().OrderBy(x=> x.BranchName);
            return entities;
        }
        public BankBranch GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }

        public BankBranch Create(BankBranch objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(BankBranch objectToUpdate)
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

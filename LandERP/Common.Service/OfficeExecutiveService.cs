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
    public interface IOfficeExecutiveService : IServiceBase<OfficeExecutive>
    {
        OfficeExecutive GetByEmail(string email);
    }
    public class OfficeExecutiveService : IOfficeExecutiveService
    {
        private readonly IOfficeExecutiveRepository repository;

        public OfficeExecutiveService(IOfficeExecutiveRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<OfficeExecutive> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public OfficeExecutive GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }

        public OfficeExecutive GetByEmail(string email)
        {
            var entity = repository.GetAll().FirstOrDefault(x => x.Email.ToLower() == email.ToLower());
            return entity;
        }

        public OfficeExecutive Create(OfficeExecutive objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(OfficeExecutive objectToUpdate)
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

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
    public interface IEmailSMSTypeService : IServiceBase<EmailSMSType>
    {
    }
    public class EmailSMSTypeService : IEmailSMSTypeService
    {
        private readonly IEmailSMSTypeRepository repository;

        public EmailSMSTypeService(IEmailSMSTypeRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<EmailSMSType> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public EmailSMSType GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public EmailSMSType Create(EmailSMSType objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(EmailSMSType objectToUpdate)
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

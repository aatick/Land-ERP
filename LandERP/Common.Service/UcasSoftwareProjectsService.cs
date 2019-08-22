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
    public interface IUcasSoftwareProjectsService : IServiceBase<UcasSoftwareProjects>
    {

    }
    public class UcasSoftwareProjectsService : IUcasSoftwareProjectsService
    {
        private readonly IUcasSoftwareProjectsRepository repository;

        public UcasSoftwareProjectsService(IUcasSoftwareProjectsRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<UcasSoftwareProjects> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }
        public UcasSoftwareProjects GetById(int id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public void Save()
        {
            repository.Commit();
        }
        public UcasSoftwareProjects Create(UcasSoftwareProjects objectToCreate)
        {
            repository.Add(objectToCreate);
            Save();
            return objectToCreate;
        }

        public void Update(UcasSoftwareProjects objectToUpdate)
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

using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IUcasSoftwareProjectsRepository : IRepository<UcasSoftwareProjects>
    {

    }
    public class UcasSoftwareProjectsRepository : RepositoryBaseCodeFirst<UcasSoftwareProjects, CommonDbContext>, IUcasSoftwareProjectsRepository
    {
        public UcasSoftwareProjectsRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

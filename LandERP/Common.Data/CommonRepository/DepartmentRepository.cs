using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {

    }
    public class DepartmentRepository : RepositoryBaseCodeFirst<Department, CommonDbContext>, IDepartmentRepository
    {
        public DepartmentRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

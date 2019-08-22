using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IOfficeExecutiveRepository : IRepository<OfficeExecutive>
    {

    }
    public class OfficeExecutiveRepository : RepositoryBaseCodeFirst<OfficeExecutive, CommonDbContext>, IOfficeExecutiveRepository
    {
        public OfficeExecutiveRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

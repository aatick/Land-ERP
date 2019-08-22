using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IGroupSetupRepository : IRepository<GroupSetup>
    {

    }
    public class GroupSetupRepository : RepositoryBaseCodeFirst<GroupSetup, CommonDbContext>, IGroupSetupRepository
    {
        public GroupSetupRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

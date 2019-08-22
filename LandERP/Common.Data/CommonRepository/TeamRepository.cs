using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface ITeamRepository : IRepository<Team>
    {

    }
    public class TeamRepository : RepositoryBaseCodeFirst<Team, CommonDbContext>, ITeamRepository
    {
        public TeamRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

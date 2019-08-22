using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IThanaRepository : IRepository<Thana>
    {

    }
    public class ThanaRepository : RepositoryBaseCodeFirst<Thana, CommonDbContext>, IThanaRepository
    {
        public ThanaRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IDistrictRepository : IRepository<District>
    {

    }
    public class DistrictRepository : RepositoryBaseCodeFirst<District, CommonDbContext>, IDistrictRepository
    {
        public DistrictRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

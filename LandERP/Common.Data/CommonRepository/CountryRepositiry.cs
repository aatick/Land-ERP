using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface ICountryRepository : IRepository<Country>
    {

    }
    public class CountryRepository : RepositoryBaseCodeFirst<Country, CommonDbContext>, ICountryRepository
    {
        public CountryRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

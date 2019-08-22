using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IProfessionRepository : IRepository<Profession>
    {

    }
    public class ProfessionRepository : RepositoryBaseCodeFirst<Profession, CommonDbContext>, IProfessionRepository
    {
        public ProfessionRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

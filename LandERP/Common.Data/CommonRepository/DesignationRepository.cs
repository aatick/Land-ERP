using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IDesignationRepository : IRepository<Designation>
    {

    }
    public class DesignationRepository : RepositoryBaseCodeFirst<Designation, CommonDbContext>, IDesignationRepository
    {
        public DesignationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

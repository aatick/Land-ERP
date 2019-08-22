using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IBankRepository : IRepository<Bank>
    {

    }
    public class BankRepository : RepositoryBaseCodeFirst<Bank, CommonDbContext>, IBankRepository
    {
        public BankRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

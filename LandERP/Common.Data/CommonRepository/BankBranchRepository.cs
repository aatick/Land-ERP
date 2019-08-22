using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IBankBranchRepository : IRepository<BankBranch>
    {

    }
    public class BankBranchRepository : RepositoryBaseCodeFirst<BankBranch, CommonDbContext>, IBankBranchRepository
    {
        public BankBranchRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Data.CommonRepository
{
    public interface IEmailSMSTypeRepository : IRepository<EmailSMSType>
    {

    }
    public class EmailSMSTypeRepository : RepositoryBaseCodeFirst<EmailSMSType, CommonDbContext>, IEmailSMSTypeRepository
    {
        public EmailSMSTypeRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {


        }
    }
}

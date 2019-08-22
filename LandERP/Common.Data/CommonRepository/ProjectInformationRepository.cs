using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IProjectInformationRepository : IRepository<ProjectInformation>
    {

    }
    public class ProjectInformationRepository : RepositoryBaseCodeFirst<ProjectInformation, CommonDbContext>, IProjectInformationRepository
    {
        public ProjectInformationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}

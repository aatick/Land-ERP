using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;

namespace Common.Data.CommonRepository
{
    public interface IProjectParentInformationRepository : IRepository<ProjectParentInformation>
    {

    }
    public class ProjectParentInformationRepository : RepositoryBaseCodeFirst<ProjectParentInformation, CommonDbContext>, IProjectParentInformationRepository
    {
        public ProjectParentInformationRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
    }
}

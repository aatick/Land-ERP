using Common.Data.CommonDataModel;
using Common.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.Data.CommonRepository
{

    public interface ISecurityRepository : IRepository<AspNetRoleModule>
    {
        IEnumerable<AspNetSecurityModule> GetAllPrentModule(string ProjectName);
        IEnumerable<AspNetSecurityModule> GetSecondPrentModule(string ProjectName);
        IEnumerable<AspNetSecurityModule> GetAllModulesForParent(int parentModuleId, int roleId);
        void CreateSecurityRole(List<AspNetRoleModule> roleModules);
    }
    public class SecurityRepository : RepositoryBaseCodeFirst<AspNetRoleModule, CommonDbContext>, ISecurityRepository
    {
        public SecurityRepository(IDatabaseFactoryCodeFirst<CommonDbContext> databaseFactory)
            : base(databaseFactory)
        {

        }
        public IEnumerable<AspNetSecurityModule> GetSecondPrentModule(string ProjectName)
        {
            // var moduels = DataContext.AspNetSecurityModules.Where(w => !w.ParentModuleId.HasValue);
            //var context = new CommonDbContext();
            var moduels = DataContext.AspNetSecurityModules.Where(w => w.MenuLevel == 2 && w.ActionName == "#" && (ProjectName == "0" || w.ProjectShortName == ProjectName));
            return moduels;
        }
        public IEnumerable<AspNetSecurityModule> GetAllPrentModule(string ProjectName)
        {
            // var moduels = DataContext.AspNetSecurityModules.Where(w => !w.ParentModuleId.HasValue);
            //var context = new CommonDbContext();
            var moduels = DataContext.AspNetSecurityModules.Where(w => w.MenuLevel == 1 && (ProjectName == "0" || w.ProjectShortName == ProjectName || w.ProjectShortName=="Basic"));
            return moduels;
        }
        public IEnumerable<AspNetSecurityModule> GetAllModulesForParent(int parentModuleId, int roleId)
        {
            //var context = new CommonDbContext();
            var allModles = DataContext.AspNetSecurityModules.Where(p => p.ParentModuleId.Value == parentModuleId).ToList();
            return allModles;
        }


        public void CreateSecurityRole(List<AspNetRoleModule> roleModules)
        {
            foreach (var roleModule in roleModules)
            {
                var context = new CommonDbContext();
                var existingRoleModule = context.AspNetRoleModules.FirstOrDefault(w => w.RoleId == roleModule.RoleId && w.ModuleId == roleModule.ModuleId);
                if (existingRoleModule == null)
                {
                    roleModule.EntryDate = DateTime.Now;
                    Add(roleModule);
                }
                else
                {
                    Update(existingRoleModule);
                }
            }

        }
    }
}

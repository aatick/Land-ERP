using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Web.ViewModels;

namespace Common.Web.Controllers
{
    public class AspNetRoleController : BaseController
    {
        #region Variables

        private readonly IAspNetRoleService aspNetRoleService;

        public AspNetRoleController(IAspNetRoleService aspNetRoleService)
        {
            this.aspNetRoleService = aspNetRoleService;
        }

        #endregion

        #region Methods


        public JsonResult SaveUserRole(int RoleId, string RoleName, string HomePage)
        {
            try
            {
                if (RoleId == 0) //Save
                {
                    var AspRol = new AspNetRole { Name = RoleName, DefaultLinkURL = HomePage, IsActive = true };
                    aspNetRoleService.Create(AspRol);
                }
                else //Edit
                {
                    var Rol = aspNetRoleService.GetById(RoleId);
                    Rol.Name = RoleName;
                    Rol.DefaultLinkURL = HomePage;
                    aspNetRoleService.Update(Rol);
                }
                return Json(new { Result = "Ok", Message = "Save successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }
        public JsonResult GetRoleInformation()
        {
            var roleInfo = aspNetRoleService.GetAll();

            List<AspNetRoleViewModel> detail = new List<AspNetRoleViewModel>();
            foreach (var r in roleInfo)
            {
                var nam = r.Name;
                var roleId = r.Id;
                string str = r.DefaultLinkURL;
                if (str == null)
                {
                    str = "";
                }
                //string last = str.Substring(str.LastIndexOf('/') + 1);

                var GetRole = new AspNetRoleViewModel() { Id = roleId, Name = nam, DefaultLinkURL = str };
                detail.Add(GetRole);
            }
            var currentURLRecords = detail.ToList();

            return Json(currentURLRecords, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RoleDelete(string RoleId)
        {
            var entity = aspNetRoleService.GetById(Convert.ToInt32(RoleId));
            string Result = "OK";
            if (ModelState.IsValid)
            {

                //aspNetRoleService.Delete(entity.RoleId);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Events

        public ActionResult Create()
        {

            var model = new AspNetRoleViewModel();
            return View(model);
        }

        #endregion
    }
}

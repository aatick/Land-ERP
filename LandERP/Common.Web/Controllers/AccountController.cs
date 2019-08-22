//using System.Transactions;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common.Data;
using Common.Service;
using Common.Service.StoredProcedure;
using Common.Web.Helpers;
using Common.Web.Models;
using Common.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Common.Web.Controllers
{

    [Authorize]

    public class AccountController : Controller
    {
        #region Variables
        private UserManager<ApplicationUser> userManager;
        private readonly IOfficeExecutiveService officeExecutiveService;
        private readonly ISPService sPService;
        private readonly IAspNetRoleService roleService;
        private readonly IAspNetUserService aspNetUserService;
        private readonly IUserInfoSPService userInfoSPService;
        private readonly ILogger loggger;
        private readonly ISecurityService securityService;
        private readonly IOrganizationService organizationService;
        private readonly IGroupSetupService groupSetupService;
        public AccountController(UserManager<ApplicationUser> userManager, ILogger loggger
            , IOfficeExecutiveService officeExecutiveService, IAspNetRoleService roleService
            , IAspNetUserService aspNetUserService, IUserInfoSPService userInfoSPService
            , ISecurityService securityService, ISPService sPService
            , IOrganizationService organizationService, IGroupSetupService groupSetupService)
        {
            this.userManager = userManager;
            this.loggger = loggger;
            this.officeExecutiveService = officeExecutiveService;
            this.roleService = roleService;
            this.aspNetUserService = aspNetUserService;
            this.userInfoSPService = userInfoSPService;
            this.securityService = securityService;
            this.sPService = sPService;
            this.organizationService = organizationService;
            this.groupSetupService = groupSetupService;
        }
        #endregion

        #region Methods
        private void LogRequest()
        {
            try
            {
                var logObject = Logger.GetLogObject();
                loggger.LogRequest(logObject);
            }
            catch (Exception ex)
            {

            }
        }

        public JsonResult GetEmployeeForRegister()
        {
            try
            {
                List<ExecutiveViewModel> Result = new List<ExecutiveViewModel>();
                {
                    var ResultList = sPService.GetEmployeeForRegister();

                    Result = ResultList.Tables[0].AsEnumerable()
                    .Select(row => new ExecutiveViewModel
                    {
                        UserId = row.Field<int>("UserId"),
                        EmployeeName = row.Field<string>("EmployeeName"),
                        EmployeeCode = row.Field<string>("EmployeeCode"),
                        Email = row.Field<string>("Email")
                    }).ToList();
                }
                return Json(Result, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult LoadRoleList()
        {
            var RoleList = new List<SelectListItem>();
            var role_List = roleService.GetAll();
            var viewrole = role_List.Select(x => x).ToList().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name.ToString()
            });
            RoleList.Add(new SelectListItem() { Text = "Please Select", Value = "0", Selected = true });
            RoleList.AddRange(viewrole);
            return Json(RoleList, JsonRequestBehavior.AllowGet);
        }

        public void UserMenuInSession(string ProjectName)
        {
            var Project = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
            var parentModules = securityService.GetAllPrentModule(ProjectName).ToList();
            var parentSecondModules = securityService.GetSecondPrentModule(ProjectName).ToList();
            var userModules = securityService.GeAllRoleModules(SessionHelper.LoggedIn_RoleId, ProjectName).ToList();
            var reportModules = securityService.GetReportModules(SessionHelper.LoggedInUserId, ProjectName).ToList();
            SessionHelper.LogSessionInfo(parentModules, parentSecondModules, userModules, reportModules, Project);
        }
        public string ProjectWiseMenu(string ProjectName)
        {
            var ReturnPage = ProjectName;
            var EmpInfo = officeExecutiveService.GetByEmail(SessionHelper.UserName);
            UserMenuInSession(ProjectName);
            return ReturnPage;
        }

        public ActionResult Project_X(string ProjectName)
        {
            var ReturnPage = ProjectWiseMenu(ProjectName);
            if (ProjectName == "0") // All Project Permission
            {
                return Json(new { Result = "Success", Message = "Successfull.", Url = "/Home/DashIndex" }, JsonRequestBehavior.AllowGet);
            }
            var ProjectHomePage = SessionHelper.ddlProjects.Where(x => x.ProjectShortName == ProjectName).FirstOrDefault().ProjectHomePage;
            return Json(new { Result = "Success", Message = "Successfull.", Url = "/Home/" + ProjectHomePage }, JsonRequestBehavior.AllowGet);
        }

        public void ReportSetting()
        {
            var header = sPService.GetDataWithoutParameter("USP_GET_REPORT_HEADER").Tables[0].AsEnumerable().Select(x => new ReportHeader()
            {
                Id = x.Field<int>(0),
                CompanyNameLeft = x.Field<int>(1),
                CompanyNameHeight = x.Field<int>(2),
                CompanyNameTop = x.Field<int>(3),
                CompanyNameWidth = x.Field<int>(4),
                CompanyNameFontSize = x.Field<int>(5),
                CompanyNameBold = x.Field<bool>(6),
                CompanyNameAlign = x.Field<string>(7),

                CompanyAddressLeft = x.Field<int>(8),
                CompanyAddressHeight = x.Field<int>(9),
                CompanyAddressTop = x.Field<int>(10),
                CompanyAddressWidth = x.Field<int>(11),
                CompanyAddressFontSize = x.Field<int>(12),
                CompanyAddressBold = x.Field<bool>(13),
                CompanyAddressAlign = x.Field<string>(14),

                ReportNameLeft = x.Field<int>(15),
                ReportNameHeight = x.Field<int>(16),
                ReportNameTop = x.Field<int>(17),
                ReportNameWidth = x.Field<int>(18),
                ReportNameFontSize = x.Field<int>(19),
                ReportNameBold = x.Field<bool>(20),
                ReportNameAlign = x.Field<string>(21),

                CompanyLogoLeft = x.Field<int>(22),
                CompanyLogoHeight = x.Field<int>(23),
                CompanyLogoTop = x.Field<int>(24),
                CompanyLogoWidth = x.Field<int>(25),

                FirstLineLeft = x.Field<int>(26),
                FirstLineTop = x.Field<int>(27),
                SecondLineLeft = x.Field<int>(28),
                SecondLineTop = x.Field<int>(29),
                FirstLineSuppress = x.Field<bool>(30),
                SecondLineSuppress = x.Field<bool>(31),

                ReportType = x.Field<string>(32),
                FirstLineWidth = x.Field<int>(33),
                SecondLineWidth = x.Field<int>(34),
                PaperType = x.Field<string>(35)
            }).ToList();

            SessionHelper.ReportHeader = header;

        }

        public JsonResult GetRegisterInformation()
        {
            try
            {
                var List_EmployeeInfo = new List<ExecutiveViewModel>();
                var empList = sPService.GetDataWithoutParameter("USP_GetUserList");

                List_EmployeeInfo = empList.Tables[0].AsEnumerable()
                .Select(row => new ExecutiveViewModel
                {
                    RowSl = row.Field<long>("RowSl"),
                    UserId = row.Field<int>("UserId"),
                    Email = row.Field<string>("UserName"),
                    EmployeeCode = row.Field<string>("EmployeeCode"),
                    EmployeeName = row.Field<string>("EmployeeName"),
                    RoleName = row.Field<string>("RoleName"),

                }).ToList();

                return Json(List_EmployeeInfo, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult RegisterInfoDelete(string UserId)
        {
            var entity = aspNetUserService.GetByUserId(Convert.ToInt32(UserId));
            string Result = "OK";
            if (ModelState.IsValid)
            {
                entity.IsRemoved = true;
                aspNetUserService.Update(entity);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public string PopulateBody(string UserName, string newPassword)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailHTMLPage/PasswordChange.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", UserName);
            body = body.Replace("{newPassword}", newPassword);
            return body;
        }

        public JsonResult RegisterInfoResetPassword(string UserId)
        {
            try
            {
                var entity = aspNetUserService.GetByUserId(Convert.ToInt32(UserId));
                string EmployeeName = string.Empty;
                EmployeeName = officeExecutiveService.GetByEmail(entity.UserName).ExecutiveName;

                Random rnd = new Random();
                int myRandomNo = rnd.Next(10000000, 99999999);
                //var myRandomNo = "12345678";

                userManager.RemovePassword(entity.Id);
                userManager.AddPassword(entity.Id, myRandomNo.ToString());

                sPService.GetDataBySqlCommand("UPDATE AspNetUsers SET Hashing='" + myRandomNo.ToString() + "' WHERE UserId=" + UserId);

                ReportHelper.SendEmail(entity.UserName, "Password Reset Confirmation", PopulateBody(EmployeeName, myRandomNo.ToString()));

                return Json(new { Result = "Ok", Message = "Password changed successfull" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult CheckCurrentPassword(string CrtPassword)
        {
            var entity = aspNetUserService.GetByUserId(Convert.ToInt32(SessionHelper.LoggedInUserId));
            string Result = "";
            var user = new ApplicationUser() { Id = entity.Id, UserName = entity.UserName, PasswordHash = entity.PasswordHash, DateCreated = Convert.ToDateTime(entity.DateCreated), Activated = entity.Activated, UserId = SessionHelper.LoggedInUserId, RoleId = entity.RoleId };
            var chk = userManager.CheckPassword(user, CrtPassword);
            if (chk == true)
            {
                Result = "OK";
            }
            else
            {
                Result = "No";
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public byte[] GetImageFromDataBase(int UserId)
        {

            var empImg = officeExecutiveService.GetById(UserId);
            return empImg != null ? empImg.Photograph : null;
        }
        public ActionResult RetrieveUserImage(int PersonId)
        {
            byte[] cover = GetImageFromDataBase(PersonId);
            if (cover != null)
            {
                return File(cover, "image/*");
            }
            string ImgPathAbsolute = HttpContext.Server.MapPath("~/images/blank-headshot.jpg");
            Image img = Image.FromFile(ImgPathAbsolute);
            byte[] blnk;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                blnk = ms.ToArray();
            }

            return File(blnk, "image/*"); ;
        }

        //[SessionExpireFilter]
        //[DisableCache]

        #endregion

        #region Events

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            LogRequest();
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            LogRequest();
            if (ModelState.IsValid)
            {
                //if (ReportHelper.CheckSoftwareExpiration())
                //{
                var entity = aspNetUserService.GetByEmail(model.UserName);
                if (entity != null)
                {
                    var user = await userManager.FindAsync(model.UserName, model.Password);
                    if (user != null)
                    {
                        await SignInAsync(user, model.RememberMe);
                        var EmpInfo = officeExecutiveService.GetByEmail(model.UserName);
                        if (EmpInfo != null)
                        {
                            #region Session Value

                            SessionHelper.LoggedInUserFullName = EmpInfo.ExecutiveName;
                            SessionHelper.LoggedInUserId = Convert.ToInt32(entity.UserId);
                            SessionHelper.UserName = model.UserName;
                            SessionHelper.LoggedInUserId_Hrm = EmpInfo.Id;
                            SessionHelper.EmployeeCode = EmpInfo.ExecutiveCode;
                            SessionHelper.LoggedInOfficeId = EmpInfo.OrganizationId;
                            SessionHelper.LoggedIn_RoleId = entity.RoleId;
                            SessionHelper.RoleName = roleService.GetById(entity.RoleId).Name;
                            var organization = organizationService.GetById(EmpInfo.OrganizationId);
                            var group = groupSetupService.GetById(organization.GroupId);
                            SessionHelper.GroupName = group.GroupName;
                            SessionHelper.GroupShortName = group.GroupName;
                            SessionHelper.OrganizationLogo = group.GroupLogo;
                            SessionHelper.OrganizationName = organization.OrganizationName;
                            SessionHelper.OrganizationShortName = organization.OrganizationShortName;
                            SessionHelper.OrganizationAddress = group.GroupAddress;
                            SessionHelper.OrgEmail = organization.OrganizationEmail;
                            SessionHelper.OrgEmailPassword = organization.OrganizationEmailPassword;
                            SessionHelper.SMSPassword = group.SMSPassword;
                            SessionHelper.SMSMobileNo = group.SMSMobileNo;
                            SessionHelper.SMSUserName = group.SMSUserName;
                            SessionHelper.OrganizationLogoPath =
                                group.GroupLogo;
                            SessionHelper.LoggedInOfficeName =
                                organization.OrganizationName;

                            SessionHelper.BusinessDate = sPService.GetBusinessDay().ToString("dd/MM/yyyy");
                            SessionHelper.TransactionDate =
                                Convert.ToDateTime(
                                    ReportHelper.FormatDateToString(sPService.GetBusinessDay()
                                        .ToString("dd/MM/yyyy")));
                            sPService.GetDataBySqlCommand("USP_AUTO_RESERVE_UNLOCK");
                            SessionHelper.Areas =
                                sPService.GetDataBySqlCommand(
                                    "SELECT DISTINCT AreaName FROM AspNetSecurityModule WHERE ISNULL(AreaName,'')<>'' AND IsActive=1")
                                    .Tables[0].AsEnumerable().Select(x => x.Field<string>(0)).ToList();

                            sPService.GetDataBySqlCommand("UPDATE ProductInformation SET ProductStatusId = 1 WHERE IsActive = 1 AND ProductStatusId = 3 AND CONVERT(DATE,ReservedUptoDate,106) = CONVERT(VARCHAR,'"+DateTime.Now+"',106)");
                          
                            #endregion

                            ReportSetting();

                            var Project = securityService.GetAllProject(SessionHelper.LoggedIn_RoleId, "0").ToList();
                            SessionHelper.UserprojectPermission(Project);

                            if (Project.Count == 1) //he has one project permission
                            {
                                var ProjectName = Project.FirstOrDefault().ProjectShortName;


                                var ReturnPage = ProjectWiseMenu(ProjectName);


                                return RedirectToAction(Project.FirstOrDefault().ProjectHomePage, "Home");
                            }
                            return RedirectToAction("Projects", "Home");
                        }
                        else
                        {
                            return RedirectToAction("UnauthorizedAccess", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
                //}
                //else
                //{
                //    ModelState.AddModelError("", "Software has been expired due to payment issue. Please contact to your vendor.");
                //}
            }
            else
            {
                // If we got this far, something failed, redisplay form
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
            }
            return View(model);
        }
        private IAuthenticationManager _authnManager;

        public IAuthenticationManager AuthenticationManager
        {
            get
            {
                if (_authnManager == null)
                    _authnManager = HttpContext.GetOwinContext().Authentication;
                return _authnManager;
            }
            set { _authnManager = value; }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            LogRequest();
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff(bool logOff)
        {
            LogRequest();
            AuthenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult RegisterIndex()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            var chngModel = new ChangePasswordViewModel();
            chngModel.UserId = SessionHelper.LoggedInUserId;

            var User = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId);
            chngModel.PersonName = User.FirstName;

            chngModel.UserName = User.UserName;

            return View(chngModel);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {

            try
            {
                var Result = "0";
                var curPwd = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId).PasswordHash;
                if (model.NewPassword == model.ConfirmPassword)
                {
                    var entity = aspNetUserService.GetByUserId(SessionHelper.LoggedInUserId);
                    userManager.RemovePassword(entity.Id);
                    userManager.AddPassword(entity.Id, model.NewPassword);
                    sPService.GetDataBySqlCommand("UPDATE AspNetUsers SET Hashing='" + model.NewPassword + "' WHERE UserId=" + SessionHelper.LoggedInUserId);
                    Result = "1";
                    return Json(Result, JsonRequestBehavior.AllowGet);

                }
                else
                {
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }
            catch
            {
                throw;
            }
        }

        public ActionResult Register()
        {
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["RoleId"] = items;
            // MapDropdownListValues();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            //if (ModelState.IsValid)
            //{
            // var person = new UserLoginViewModel();

            //  var emp = brokerEmployeeService.GetAll().Where(s=>s.Id == model.UserId).FirstOrDefault();
            //person.UserLoginId = emp.Id;
            //person.UserId = emp.Id;
            // person.PersonType = "User";
            // person.PersonEmail = emp.Email;
            // model.RoleId = model.RoleId;



            //   if (emp != null)
            //{
            try
            {
                var user = new ApplicationUser() { UserId = model.UserId, UserName = model.UserName, FirstName = model.PersonName, RoleId = model.RoleId };
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    sPService.GetDataBySqlCommand("UPDATE AspNetUsers SET Hashing='" + model.Password + "' WHERE UserId=" + model.UserId);
                    return Json(new { Result = "OK", Message = "Login Created successfully." }, JsonRequestBehavior.AllowGet);
                    //await SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    var msg = "";
                    foreach (var r in result.Errors)
                    {
                        msg = string.Format("{0}<br/>{1}", msg, r);
                    }
                    return Json(new { Result = "ERROR", Message = msg }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //}
            //else
            //    return Json(new { Result = "ERROR", Message = "Invalid Email." }, JsonRequestBehavior.AllowGet);

            // }

            // return Json(new { Result = "ERROR", Message = "Please correct required fields." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegisterEdit(int UserId)
        {
            var per = aspNetUserService.GetByUserId(UserId);
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["RoleId"] = items;
            var regModel = new RegisterModel();
            regModel.UserId = Convert.ToInt32(per.UserId);
            regModel.PersonName = per.FirstName;
            regModel.UserName = per.UserName;
            regModel.RoleId = per.RoleId;
            return View(regModel);

        }

        [HttpPost]
        public ActionResult RegisterEdit(RegisterModel model)
        {
            try
            {
                var personList = aspNetUserService.GetByUserId(model.UserId); //aspNetUserService.GetByPerson(model.PersonId, model.PersonType);
                personList.RoleId = model.RoleId;
                personList.UserName = model.UserName;
                aspNetUserService.Update(personList);
                return Json(new { Result = "OK", Message = "Login update successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }
        public JsonResult GetBrokerEmployeeForRegister()
        {
            try
            {
                var employees = sPService.GetDataWithoutParameter("USP_GetBrokerEmployeeForRegister");
                var employeeList = employees.Tables[0].AsEnumerable()
                    .Select(row =>
                        new
                        {
                            UserId = row.Field<int>("UserId"),
                            EmployeeName = row.Field<string>("EmployeeName"),
                            EmployeeCode = row.Field<string>("EmployeeCode"),
                            Email = row.Field<string>("Email")
                        }
                    ).ToList();
                return Json(employeeList, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}

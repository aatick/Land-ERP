using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using Common.Web.Helpers;

namespace Common.Web.Controllers
{
    public class OfficeExecutiveController : BaseController
    {
        private readonly IOfficeExecutiveService officeExecutiveService;
        private readonly IOrganizationService organizationService;
        private readonly IDesignationService designationService;
        private readonly IDepartmentService departmentService;
        private readonly IGenderService genderService;
        private readonly IDivisionService divisionService;
        private readonly IDistrictService districtService;
        private readonly IThanaService thanaService;
        private readonly ICountryService countryService;
        private readonly ITeamService teamService;
        private readonly IProfessionService professionService;
        private readonly ISPService spService;

        public OfficeExecutiveController(IOfficeExecutiveService officeExecutiveService, IOrganizationService organizationService
            , IDesignationService designationService, IDepartmentService departmentService
            , IGenderService genderService, IDivisionService divisionService, IDistrictService districtService
            , IThanaService thanaService, ICountryService countryService, ITeamService teamService
            , IProfessionService professionService, ISPService spService)
        {
            this.officeExecutiveService = officeExecutiveService;
            this.organizationService = organizationService;
            this.designationService = designationService;
            this.departmentService = departmentService;
            this.genderService = genderService;
            this.divisionService = divisionService;
            this.districtService = districtService;
            this.thanaService = thanaService;
            this.countryService = countryService;
            this.teamService = teamService;
            this.professionService = professionService;
            this.spService = spService;
        }
        public ActionResult Index(int? id, int view = 0)
        {
            ViewBag.Organizations = organizationService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Designations = designationService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Departments = departmentService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Genders = genderService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Divisions = divisionService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Districts = districtService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Thanas = thanaService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.Countries = countryService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.TeamList = teamService.GetAll().ToList();
            var executives = officeExecutiveService.GetAll().Where(x => x.IsActive).ToList();
            ViewBag.ExecutiveList = executives;
            var executive = new OfficeExecutive() { Id = 0 };
            if (id.HasValue)
            {
                executive = executives.FirstOrDefault(x => x.Id == id);
            }
            ViewBag.Executive = executive;
            ViewBag.IsViewMode = view;
            return View();
        }
        public JsonResult ExecutiveSetup(OfficeExecutive executive, HttpPostedFileBase photo, HttpPostedFileBase sign, string joiningDate)
        {

            executive.PresentThanaId = executive.PresentThanaId == 0 ? null : executive.PresentThanaId;
            executive.PermanentThanaId = executive.PermanentThanaId == 0 ? null : executive.PermanentThanaId;
            executive.TeamId = executive.TeamId == 0 ? null : executive.TeamId;

            var joining = ReportHelper.FormatDate(joiningDate);
            executive.JoiningDate = joining;
            if (executive.Id == 0)
            {
                executive.EntryUserId = SessionHelper.LoggedInUserId;
                executive.EntryDate = DateTime.Now;
                executive.IsActive = true;
                if (photo != null)
                    executive.Photograph = ReportHelper.ConvertStreamToByte(photo.InputStream);
                if (sign != null)
                    executive.Signature = ReportHelper.ConvertStreamToByte(sign.InputStream);

                if (
                    officeExecutiveService.GetAll()
                        .FirstOrDefault(x => x.ExecutiveCode.ToLower() == executive.ExecutiveCode.ToLower()) != null)
                    return Json(new { Status = false, Message = "Duplicate executive code." }, JsonRequestBehavior.AllowGet);

                officeExecutiveService.Create(executive);
                return Json(new { Status = true, Message = "New executive created successfully." },
                        JsonRequestBehavior.AllowGet);
            }
            var existExecutive = officeExecutiveService.GetById(executive.Id);

            existExecutive.ExecutiveName = executive.ExecutiveName;
            existExecutive.ExecutiveCode = executive.ExecutiveCode;
            existExecutive.OrganizationId = executive.OrganizationId;
            existExecutive.DesignationId = executive.DesignationId;
            existExecutive.DepartmentId = executive.DepartmentId;
            existExecutive.JoiningDate = executive.JoiningDate;
            existExecutive.FatherName = executive.FatherName;
            existExecutive.MotherName = executive.MotherName;
            existExecutive.GenderId = executive.GenderId;
            existExecutive.PresentAddress = executive.PresentAddress;
            existExecutive.PresentThanaId = executive.PresentThanaId;
            existExecutive.PermanentAddress = executive.PermanentAddress;
            existExecutive.PermanentThanaId = executive.PermanentThanaId;
            existExecutive.CountryId = executive.CountryId;
            existExecutive.Email = executive.Email;
            existExecutive.Mobile = executive.Mobile;
            existExecutive.TeamId = executive.TeamId;
            if (photo != null)
                existExecutive.Photograph = ReportHelper.ConvertStreamToByte(photo.InputStream);
            if (sign != null)
                existExecutive.Signature = ReportHelper.ConvertStreamToByte(sign.InputStream);
            existExecutive.UpdateUserId = SessionHelper.LoggedInUserId;
            existExecutive.UpdateDate = DateTime.Now;

            if (
                officeExecutiveService.GetAll()
                    .FirstOrDefault(x => x.ExecutiveCode.ToLower() == executive.ExecutiveCode.ToLower() && x.Id != executive.Id) !=
                null)
                return Json(new { Status = false, Message = "Duplicate executive code." }, JsonRequestBehavior.AllowGet);

            officeExecutiveService.Update(existExecutive);
            return Json(new { Status = true, Message = "Executive updated successfully." },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Team(int id = 0)
        {
            var team = new Team() { Id = 0 };
            if (id > 0)
                team = teamService.GetById(id);
            ViewBag.Team = team;
            ViewBag.TeamList = teamService.GetAll().ToList();
            return View();
        }
        public JsonResult TeamSetup(Team team)
        {
            var msg = "";
            if (team.Id == 0)
            {
                if (
                    teamService.GetAll()
                        .FirstOrDefault(x => x.TeamName == team.TeamName) != null)
                {
                    return Json(new { Message = "This team already exists.", Status = false },
                        JsonRequestBehavior.AllowGet);
                }
                team.EntryDate = DateTime.Now;
                team.EntryUserId = SessionHelper.LoggedInUserId;
                team.IsActive = true;
                teamService.Create(team);
                msg = "New team created successfully.";
            }
            else
            {
                if (
                    teamService.GetAll()
                        .FirstOrDefault(x => x.TeamName == team.TeamName && x.Id != team.Id) != null)
                {
                    return Json(new { Message = "This team already exists.", Status = false },
                        JsonRequestBehavior.AllowGet);
                }
                var existTeam = teamService.GetById(team.Id);
                existTeam.TeamName = team.TeamName;
                existTeam.UpdateDate = DateTime.Now;
                existTeam.UpdateUserId = SessionHelper.LoggedInUserId;
                existTeam.IsActive = true;
                teamService.Update(existTeam);
                msg = "Team updated successfully.";
            }
            return Json(new { Message = msg, Status = true },
                        JsonRequestBehavior.AllowGet);
        }
        public ActionResult Profession(int id = 0)
        {
            var profession = new Profession() { Id = 0 };
            if (id > 0)
                profession = professionService.GetById(id);
            ViewBag.Profession = profession;
            return View();
        }
        public JsonResult ProfessionSetup(Profession profession)
        {
            try
            {
                var msg = "";
                if (profession.Id == 0)
                {
                    if (
                        professionService.GetAll()
                            .FirstOrDefault(x => x.ProfessionName == profession.ProfessionName) != null)
                    {
                        return Json(new { Message = "This profession already exists.", Status = false },
                            JsonRequestBehavior.AllowGet);
                    }
                    profession.IsActive = true;
                    professionService.Create(profession);
                    msg = "New profession created successfully.";
                }
                else
                {
                    if (
                        professionService.GetAll()
                            .FirstOrDefault(x => x.ProfessionName == profession.ProfessionName && x.Id != profession.Id) != null)
                    {
                        return Json(new { Message = "This profession already exists.", Status = false },
                            JsonRequestBehavior.AllowGet);
                    }
                    var existProfession = professionService.GetById(profession.Id);
                    existProfession.ProfessionName = profession.ProfessionName;
                    existProfession.IsActive = true;
                    professionService.Update(existProfession);
                    msg = "Profession updated successfully.";
                }
                return Json(new { Message = msg, Status = true },
                    JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Message = ex.GetErrorMessage(), Status = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetProfessionList()
        {
            return Json(professionService.GetAll().ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteProfession(int id)
        {
            try
            {
                var client = spService.GetDataWithParameter(new {ID = id}, "USP_CHECK_PROFESSION_USE").Tables[0].AsEnumerable();
                if (!client.Any())
                {
                    var profession = professionService.GetById(id);
                    profession.IsActive = false;
                    professionService.Update(profession);
                    return Json(new { Status = true, Message = "Profession delete successfull." }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { Status = false, Message = "Delete deny.There are client/joint client/nominee with this profession." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = false, Message = ex.GetErrorMessage() }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
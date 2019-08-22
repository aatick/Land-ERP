using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using Common.Web.Helpers;

namespace Common.Web.Controllers
{
    public class ReportsController : BaseController
    {

        private readonly ISPService sPService;
        private readonly IOrganizationService organizationService;
        private readonly IDepartmentService departmentService;
        private readonly IDesignationService designationService;
        private readonly ITeamService teamService;

        public ReportsController(ISPService sPService, IOrganizationService organizationService
                , IDepartmentService departmentService, IDesignationService designationService, ITeamService teamService)
        {
            this.sPService = sPService;
            this.organizationService = organizationService;
            this.departmentService = departmentService;
            this.designationService = designationService;
            this.teamService = teamService;
        }
        public void Get_GeneralReport(int reportNo, string Qtype, string exportType)
        {
            if (Qtype == "Occupation" || Qtype == "Relation" || Qtype == "Designation" || Qtype == "Department")
            {
                var data =
                sPService.GetDataWithParameter(
                    new { Qtype = Qtype },
                    "Rpt_Get_Occupation_Relation_Designation").Tables[0];
                var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
                if (Qtype == "Occupation")
                {
                    ReportHelper.ShowReport(data, exportType, "rpt_Occupation.rpt", "rpt_Occupation", reportParam);
                }
                else if (Qtype == "Relation")
                {
                    ReportHelper.ShowReport(data, exportType, "rpt_Relation.rpt", "rpt_Relation", reportParam);
                }
                else if (Qtype == "Designation")
                {
                    ReportHelper.ShowReport(data, exportType, "rpt_Designation.rpt", "rpt_Designation", reportParam);
                }
                else if (Qtype == "Department")
                {
                    ReportHelper.ShowReport(data, exportType, "rpt_Department.rpt", "rpt_Department", reportParam);
                }
            }

            else
            {
                var data2 =
              sPService.GetDataWithParameter(
                  new { Qtype = Qtype },
                  "Rpt_Get_Country_Divition_District_Thana").Tables[0];
                var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };

                if (Qtype == "Country")
                {
                    ReportHelper.ShowReport(data2, exportType, "rpt_Country.rpt", "rpt_Country", reportParam);
                }
                else if (Qtype == "Division")
                {
                    ReportHelper.ShowReport(data2, exportType, "rpt_Division.rpt", "rpt_Division", reportParam);
                }
                else if (Qtype == "District")
                {
                    ReportHelper.ShowReport(data2, exportType, "rpt_District.rpt", "rpt_District", reportParam);
                }
                else if (Qtype == "Thana")
                {
                    ReportHelper.ShowReport(data2, exportType, "rpt_Thana.rpt", "rpt_Thana", reportParam);
                }
            }
        }


        public void GetBankWiseAllBranch(int reportNo, string exportType)
        {
            var data =
                sPService.GetDataWithoutParameter(
                    "RPT_GetBankWiseAllBranch").Tables[0];
            var reportParam = new Dictionary<string, object> { { "param_orgName", "Ucas" } };
            ReportHelper.ShowReport(data, exportType, "rpt_Bank_BranchList.rpt", "rpt_BrokerInfo", reportParam);
        }


        public void Index()
        {
            var report = SessionHelper.Report;
            var exportOption = SessionHelper.ReportFormat;
            var exportFileName = SessionHelper.ReportExportName;
            System.Web.HttpContext.Current.Response.BufferOutput = true;
            report.ExportToHttpResponse(exportOption, System.Web.HttpContext.Current.Response, false, exportFileName);
            report.Dispose();
            report.Close();
        }

        public ActionResult GnlRpt()
        {
            var moduleid = sPService.GetSecurityModuleByControllerAction("Reports", "GnlRpt");
            ViewBag.Reports = sPService.GetDataWithParameter(new
            {
                USER_ID = SessionHelper.LoggedInUserId,
                ReportModuleId = moduleid
            }, "USP_GET_USER_ACCESSED_REPORT").Tables[0].AsEnumerable().Select(x => new ReportInformation()
            {
                Id = x.Field<int>(0),
                ReportName = x.Field<string>(1),
                SerialNo = x.Field<int>(2)
            }).ToList();
            return View();
        }

        public ActionResult Employee()
        {
            ViewBag.Organizations = organizationService.GetAll().ToList();
            ViewBag.Departments = departmentService.GetAll().ToList();
            ViewBag.Designations = designationService.GetAll().ToList();
            ViewBag.Teams = teamService.GetAll().ToList();
            return View();
        }
        public void ShowEmployeeReport(int orgId, string type, int departmentId, int designationId, int teamId)
        {
            var data =
                sPService.GetDataWithParameter(
                    new
                    {
                        ORG_ID = orgId,
                        DEPARTMENT_ID = departmentId,
                        DESIGNATION_ID = designationId,
                        TEAM_ID = teamId
                    },
                    "USP_RPT_EMPLOYEE_LIST").Tables[0];
            ReportHelper.ShowReport(data, type, "rptEmployeeList.rpt", "EmployeeList");
        }
    }
}
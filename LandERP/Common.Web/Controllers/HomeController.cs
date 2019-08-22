using System;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service.StoredProcedure;
using Common.Web.Helpers;

namespace Common.Web.Controllers
{
    public class HomeController : BaseController
    {
        #region Variables
        private readonly ISPService spService;

        public HomeController(ISPService spService)
        {
            this.spService = spService;
        }

        #endregion

        #region Methods
        public JsonResult GetEmployeeNotification()
        {
            try
            {
                var param = new { EmployeeId = SessionHelper.LoggedInUserId };
                var NotiListData = spService.GetDataWithParameter(param, "GetEmployeeNotification");

                var NotiList = NotiListData.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    employee_id = row.Field<int>("employee_id"),
                    LoactionURL = row.Field<string>("LoactionURL"),
                    NotiNo = row.Field<int>("NotiNo"),
                    NotiType = row.Field<string>("NotiType"),
                    NotiTex = row.Field<string>("NotiTex")
                }).ToList();

                return Json(NotiList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }

        }



        #endregion

        #region Events

        public ActionResult LandIndex()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult InvIndex()
        {
            return View();
        }
        public ActionResult UnauthorizedAccess()
        {
            return View();
        }
        public ActionResult DashIndex()
        {
            return View();
        }
        public ActionResult Projects()
        {

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #endregion
        public JsonResult CheckSession()
        {
            if (System.Web.HttpContext.Current.Session[SessionKeys.USER_ID] == null)
                return Json(false, JsonRequestBehavior.AllowGet);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult Deed(int orgId = 0, int projectId = 0, string fileNo = "", int deductionPercentage = 0, int refundDuration = 0, int handOverYear = 0, int agreementStatus = 1, int isSavedData = 0)
        {
            if (isSavedData == 0)
            {
                if (agreementStatus == 1)
                {
                    var data = spService.GetDataWithParameter(new
                    {
                        ORG_ID = orgId,
                        PROJECT_ID = projectId,
                        FILE_NO = fileNo
                    }, "USP_GET_DEED_OF_AGREEMENT");
                    ViewBag.Deed = data.Tables[0];
                    ViewBag.JointClient = data.Tables[1];
                    ViewBag.DeductionPercentage = deductionPercentage;
                    ViewBag.RefundDuration = refundDuration;
                    ViewBag.HandOverYear = handOverYear;
                    return View();
                }
                else
                {
                    var data = spService.GetDataWithParameter(new
                    {
                        ORG_ID = orgId,
                        PROJECT_ID = projectId,
                        FILE_NO = fileNo,
                        AGREEMENT_STATUS = agreementStatus
                    }, "USP_GET_AGREEMENT_DATA").Tables[0];
                    var agreementData = "";
                    if (data.Rows.Count > 0)
                    {
                        agreementData = data.Rows[0][0].ToString();
                    }
                    return Content(agreementData);
                }
            }
            else
            {
                var data = spService.GetDataWithParameter(new
                {
                    ORG_ID = orgId,
                    PROJECT_ID = projectId,
                    FILE_NO = fileNo,
                    AGREEMENT_STATUS = agreementStatus + 1
                }, "USP_GET_AGREEMENT_DATA").Tables[0];
                var agreementData = "";
                if (data.Rows.Count > 0)
                {
                    agreementData = data.Rows[0][0].ToString();
                }
                return Content(agreementData);
            }
        }
    }
}

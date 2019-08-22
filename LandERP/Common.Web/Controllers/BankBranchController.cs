using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Common.Service;
using Common.Service.StoredProcedure;
using Common.Web.Helpers;


namespace Common.Web.Controllers
{
    public class BankBranchController : BaseController
    {

        private readonly ISPService spService;

        public BankBranchController(ISPService spService)
        {

            this.spService = spService;
        }

        public JsonResult GetBankAccountInfo()
        {
            try
            {
                var param = new
                {
                    OrganizationId = 0,
                    RoutingNo = "",
                    BankBranchId = 0,
                    BankId = 0,
                };
                var Master = spService.GetDataWithParameter(param, "USP_GET_BANK_ACCOUNT_LIST");
                var BranchInfo = Master.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    RowSl = row.Field<string>("RowSl"),
                    Id = row.Field<int>("Id"),
                    BankBranchId = row.Field<int?>("BankBranchId"),
                    OrganizationId = row.Field<int?>("OrganizationId"),
                    BankId = row.Field<int?>("BankId"),
                    BranchName = row.Field<string>("BranchName"),
                    RoutingNo = row.Field<string>("RoutingNo"),
                    BankName = row.Field<string>("BankName"),
                    OrganizationName = row.Field<string>("OrganizationName"),
                    BankAccountNo = row.Field<string>("BankAccountNo"),
                    OrganizationShortName = row.Field<string>("OrganizationShortName"),

                }).ToList();
                return Json(BranchInfo, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveBankAccount(int OrgBankAccountId, int BankBranchId, int OrganizationId, string BankAccountNo)
        {
            try
            {
                if (BankBranchId == 0)
                {
                    return Json(new { Result = "ERROR", Message = "Select Bank Branch" }, JsonRequestBehavior.AllowGet);
                }
                else if (OrganizationId == 0)
                {
                    return Json(new { Result = "ERROR", Message = "Select Organization" }, JsonRequestBehavior.AllowGet);
                }
                else if (BankAccountNo == "" || BankAccountNo == null)
                {
                    return Json(new { Result = "ERROR", Message = "Insert Bank Account No" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (OrgBankAccountId == 0)
                    {
                        spService.GetDataBySqlCommand("INSERT INTO OrganizationBankAccount(BankBranchId,OrganizationId,BankAccountNo,EntryDate,EntryUserId,IsActive)Values(" + BankBranchId + "," + OrganizationId + ",'" + BankAccountNo + "','" + DateTime.Now + "'," + SessionHelper.LoggedInUserId + ",1)");
                        return Json(new { Result = "Success", Message = "Save Successfull." }, JsonRequestBehavior.AllowGet);
                    }
                   else
                    {
                        spService.GetDataBySqlCommand("UPDATE OrganizationBankAccount SET BankBranchId = " + BankBranchId + ",OrganizationId = " + OrganizationId + ",BankAccountNo = '" + BankAccountNo + "',UpdateDate = GETDATE(),UpdateUserId= " + SessionHelper.LoggedInUserId + " WHERE Id = " + OrgBankAccountId + "");
                        return Json(new { Result = "Success", Message = "Update Successfull." }, JsonRequestBehavior.AllowGet);
                    }
                }
                
            }
            catch(Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetDistrictList(int DivisionId)
        {
            try
            {
                var DistrictList = spService.GetDataBySqlCommand("SELECT D.Id,D.DistrictName FROM District AS D WHERE D.IsActive =1 AND D.DivisionId = " + DivisionId + "").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), DistrictName = x.Field<string>("DistrictName") }).ToList();

                return Json(new { Result = "Success", Message = "", DistrictList = DistrictList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetddlThanaList(int DistrictId)
        {
            try
            {
                var ThanList = spService.GetDataBySqlCommand("SELECT T.Id,T.ThanaName FROM Thana AS T WHERE T.IsActive =1 AND T.DistrictId = " + DistrictId + "").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), ThanaName = x.Field<string>("ThanaName") }).ToList();

                return Json(new { Result = "Success", Message = "", ThanList = ThanList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetBankwiseBranchListddl(int BankId)
        {
            try
            {
                var BranchList = spService.GetDataBySqlCommand("SELECT B.Id,B.BankId,B.BranchName FROM BankBranch AS B WHERE B.IsActive = 1 AND B.BankId = " + BankId + "").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), BranchName = x.Field<string>("BranchName") }).ToList();

                return Json(new { Result = "Success", Message = "", BranchList = BranchList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBankwiseBranchList(int BankId, int ThanaId, string RoutingNo)
        {
            try
            {
                var param = new
                {
                    TrxMode = "GET",
                    BranchId = 0,
                    BankId = BankId,
                    ThanaId = ThanaId,
                    BranchName = "",
                    Address = "",
                    RoutingNo = RoutingNo,
                    EntryDate = "",
                    EntryUserId = 0,
                };
                var Master = spService.GetDataWithParameter(param, "USP_INSERT_UPDATE_GET_BankBranch");
                var BranchInfo = Master.Tables[0].AsEnumerable()
                .Select(row => new
                {
                    RowSl = row.Field<string>("RowSl"),
                    Id = row.Field<int>("Id"),
                    ThanaId = row.Field<int?>("ThanaId"),
                    ThanaName = row.Field<string>("ThanaName"),
                    BankId = row.Field<int>("BankId"),
                    DistrictId = row.Field<int?>("DistrictId"),
                    DivisionId = row.Field<int?>("DivisionId"),
                    BankName = row.Field<string>("BankName"),
                    BranchName = row.Field<string>("BranchName"),
                    Address = row.Field<string>("Address"),
                    RoutingNo = row.Field<string>("RoutingNo")


                }).ToList();
                return Json(BranchInfo, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        public JsonResult BankBranchDelete(int Id)
        {
            try
            {
                spService.GetDataBySqlCommand("UPDATE BankBranch SET IsActive = 0,UpdateDate = '" + DateTime.Now + "',UpdateUserId = " + SessionHelper.LoggedInUserId + " WHERE Id = " + Id + "");

                return Json(new { Result = "Success", Message = "Branch Delete Successfull." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveUpdateBankBranchInfo(string TrxMode, int BankBranchId, int BankId, string BranchName, string ThanaId, string Address, string RoutingNo)
        {
            try
            {

                var param = new
                {
                    TrxMode = TrxMode,
                    BranchId = BankBranchId,
                    BankId = BankId,
                    ThanaId = ThanaId,
                    DistrictId = 0,
                    BranchName = BranchName,
                    Address = Address,
                    RoutingNo = RoutingNo,
                    EntryDate = DateTime.Now,
                    EntryUserId = SessionHelper.LoggedInUserId,
                };

                var Msg = spService.GetDataWithParameter(param, "USP_INSERT_UPDATE_GET_BankBranch").Tables[0].Rows[0][0].ToString();
                return Json(new { Result = "Success", Message = Msg }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "Error", Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Branch()
        {
            ViewBag.DivisionList = spService.GetDataBySqlCommand(" SELECT D.Id,D.DivisionName FROM Division AS D WHERE D.IsActive = 1 ORDER BY D.DivisionName").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), DivisionName = x.Field<string>("DivisionName") }).ToList();
            ViewBag.BankList = spService.GetDataBySqlCommand("SELECT B.Id,B.BankName,B.BankShortName FROM Bank AS B WHERE B.IsActive = 1 ORDER BY B.BankName").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), BankName = x.Field<string>("BankName") }).ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewData["Thanalist"] = items;
            return View();
        }

        public ActionResult OrgBank()
        {
            ViewBag.BankList = spService.GetDataBySqlCommand("SELECT B.Id,B.BankName,B.BankShortName FROM Bank AS B WHERE B.IsActive = 1 ORDER BY B.BankName").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), BankName = x.Field<string>("BankName") }).ToList();
            ViewBag.OrgList = spService.GetDataBySqlCommand("SELECT R.Id,R.GroupId,R.OrganizationName,R.OrganizationShortName FROM Organization AS R WHERE R.IsActive = 1 ORDER BY R.OrganizationName").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>("Id"), OrganizationName = x.Field<string>("OrganizationName") }).ToList();
             IEnumerable<SelectListItem> items = new SelectList(" ");
             ViewData["BranchList"] = items;
            
            return View();
        }
    }
}

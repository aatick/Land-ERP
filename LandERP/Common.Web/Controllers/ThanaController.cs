using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Data.CommonDataModel;
using Common.Service;
using Common.Service.StoredProcedure;
using System.Data;

namespace Common.Web.Controllers
{
    public class ThanaController : BaseController
    {
        private readonly IThanaService thanaService;
        private readonly IDivisionService divisionService;
        private readonly IDistrictService districtService;
        private readonly ISPService spService;
        public ThanaController(IThanaService thanaService, IDivisionService divisionService
            , IDistrictService districtService, ISPService spService)
        {
            this.thanaService = thanaService;
            this.divisionService = divisionService;
            this.districtService = districtService;
            this.spService = spService;
        }
        public JsonResult ThanaDelete(string Id)
        {
            var result = 0;
            try
            {
                var thana = thanaService.GetById(Convert.ToInt32(Id));
                thana.IsActive = false;
                thanaService.Update(thana);
                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult EditThana(string DistrictId, string ThanaName, string ThanaId)
        {
            var result = 0;
            try
            {
                var thana = thanaService.GetById(Convert.ToInt32(ThanaId));

                thana.DistrictId = Convert.ToInt32(DistrictId);
                thana.ThanaName = ThanaName;
                thanaService.Update(thana);
                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveThana(string DistrictId, string Thana)
        {
            var result = 0;
            try
            {

                var tha = new Thana()
                {
                    DistrictId = Convert.ToInt32(DistrictId),
                    ThanaName = Thana,
                    IsActive = true,
                };
                thanaService.Create(tha);
                result = 1;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                result = 0;
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetDistrictList(string DivisionId)
        {
            if (DivisionId == "")
            {
                DivisionId = "0";
            }
            var DistrictList = districtService.GetAll().Where(s => s.DivisionId == Convert.ToInt32(DivisionId));
            return Json(DistrictList, JsonRequestBehavior.AllowGet);
        }

        //GetThanaList

        public JsonResult GetddlThanaList(string DistrictId)
        {
            if (DistrictId == "")
            {
                DistrictId = "0";
            }
            var ThanaList = thanaService.GetAll().Where(t => t.DistrictId == (Convert.ToInt32(DistrictId)));
            return Json(ThanaList, JsonRequestBehavior.AllowGet);

        }
        public class ThanaModel
        {
            public int Id { get; set; }
            public int SLNo { get; set; }
            public string ThanaName { get; set; }
            public string DistrictName { get; set; }
        }
        public JsonResult GetThanaList(string DistrictId, string DivisionId)
        {
            var Thana = spService.GetDataWithParameter(new
            {
                DIVISION_ID = DivisionId,
                DISTRICT_ID = DistrictId
            }, "USP_GET_THANA_LIST").Tables[0].AsEnumerable().Select(x => new { Id = x.Field<int>(0), ThanaName = x.Field<string>(1), DistrictName = x.Field<string>(2) }).ToList();
            return Json(Thana, JsonRequestBehavior.AllowGet);
        }
        //
        // GET: /LookupThana/
        public ActionResult Index()
        {
            ViewBag.DivisionList = divisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            return View();
        }

        //
        // GET: /LookupThana/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /LookupThana/Create
        public ActionResult Create()
        {
            ViewBag.DivisionList = divisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            return View();
        }

        //
        // POST: /LookupThana/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LookupThana/Edit/5
        public ActionResult Edit(int Id)
        {
            var model = thanaService.GetById(Id);
            ViewBag.DivisionList = divisionService.GetAll().ToList();
            IEnumerable<SelectListItem> items = new SelectList(" ");
            ViewData["Districtlist"] = items;
            ViewBag.DistrictId = model.DistrictId;
            ViewBag.ThanaName = model.ThanaName;
            ViewBag.DivisionId = districtService.GetAll().Where(d => d.Id == model.DistrictId).FirstOrDefault().DivisionId;
            ViewBag.ThanaId = Id;
            return View();
        }

        //
        // POST: /LookupThana/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LookupThana/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /LookupThana/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

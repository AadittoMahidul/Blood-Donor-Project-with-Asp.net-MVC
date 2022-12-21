using BloodDonorManagementSystem.Models;
using BloodDonorManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BloodDonorManagementSystem.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly BloodDonorDbContext db = new BloodDonorDbContext();
        //GET: Hospitals
        public ActionResult Index()
        {
            var data = db.Hospitals
                .Include(x => x.HospitalPatients.Select(y => y.Patient))
                .ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            ViewBag.Patients = db.Patients.ToList();
            var data = new HospitalInputModel();
            data.Patients.Add(new PatientViewModel());
            return View(data);
        }
        [HttpPost]
        public ActionResult Create(HospitalInputModel model)
        {
            if (ModelState.IsValid)
            {
                var h = new Hospital
                {
                    HospitalName = model.HospitalName,
                    Address = model.Address,
                    Email = model.Email,
                    Location = model.Location
                };
                foreach (var x in model.Patients)
                {
                    h.HospitalPatients.Add(new HospitalPatient { PatientId = x.PatientId });
                }
                if (model.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Guid.NewGuid() + ext;
                    model.Picture.SaveAs(Path.Combine(Server.MapPath("~/Assets"), fileName));
                    h.Picture = fileName;
                }
                db.Hospitals.Add(h);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Patients = db.Patients.ToList();

            return View(model);
        }
        public JsonResult GetPaymentBill(int id)
        {
            var t = db.Patients.FirstOrDefault(x => x.PatientId == id);
            return Json(t == null ? 0 : t.PaymentBill);
        }
        public ActionResult CreateNewField(HospitalInputModel data)
        {
            ViewBag.Patients = db.Patients.ToList();
            data.Patients.Add(new PatientViewModel());
            return PartialView(data);
        }
        public ActionResult CreateV1()
        {

            return View();
        }
        public PartialViewResult CreateHospital()
        {
            ViewBag.Patients = db.Patients.ToList();

            return PartialView("_Hospital");
        }
    }
}
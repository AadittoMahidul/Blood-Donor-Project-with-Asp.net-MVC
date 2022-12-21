using BloodDonorManagementSystem.Models;
using BloodDonorManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonorManagementSystem.Controllers
{
    public class PatientsController : Controller
    {
        BloodDonorDbContext db = new BloodDonorDbContext();
        // GET: Patients
        public ActionResult Index()
        {
            var data = db.Patients
                .Select(w =>
                    new PatientViewModel
                    {
                        PatientId = w.PatientId,
                        PatientName = w.PatientName,
                        Address = w.Address,
                        Email = w.Email,
                        Phone = w.Phone,
                        AdmitDate = w.AdmitDate,
                        EndDate = w.EndDate,
                        PaymentBill = w.PaymentBill,
                        Picture=w.Picture,
                        IsAvailable = w.IsAvailable
                    }
               )
                .ToList();
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(PatientInputModel data)
        {
            if (ModelState.IsValid)
            {
                if (data.Picture.ContentLength > 0)
                {
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Guid.NewGuid() + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Assets"), fileName);
                    data.Picture.SaveAs(savePath);
                    db.Patients.Add(new Patient
                    {
                        PatientName = data.PatientName,
                        Address = data.Address,
                        Email = data.Email,
                        Phone = data.Phone,                                              
                        AdmitDate = data.AdmitDate,
                        EndDate = data.EndDate,                       
                        PaymentBill = data.PaymentBill,
                        Picture = fileName,
                        IsAvailable = data.IsAvailable
                    });
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(data);
        }
        public ActionResult Edit(int id)
        {
            var t = db.Patients.First(x => x.PatientId == id);
            
            ViewBag.CurrentPic = t.Picture;
            return View(new PatientEditModel { PatientId = t.PatientId, PatientName = t.PatientName, Address = t.Address, Email = t.Email, Phone = t.Phone, AdmitDate = t.AdmitDate,EndDate= t.EndDate,PaymentBill = t.PaymentBill, IsAvailable=t.IsAvailable/*, Picture = t.Picture */});
        }
        [HttpPost]
        public ActionResult Edit(PatientEditModel t)
        {
            var Patient = db.Patients.First(x => x.PatientId == t.PatientId);
            if (ModelState.IsValid)
            {

                Patient.PatientName = t.PatientName;
                Patient.Address = t.Address;
                Patient.Email = t.Email;
                Patient.Phone = t.Phone;
                Patient.AdmitDate = t.AdmitDate;
                Patient.EndDate = t.EndDate;
                Patient.PaymentBill = t.PaymentBill;
                Patient.IsAvailable = t.IsAvailable;
                if (t.Picture != null)
                {
                    string ext = Path.GetExtension(t.Picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.Picture.SaveAs(Server.MapPath("~/Assets/") + f);
                    Patient.Picture = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = Patient.Picture;
            //ViewBag.Bloods = db.Bloods.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Patients.Include(x => x.HospitalPatients).First(x => x.PatientId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Patient t = new Patient { PatientId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //public ActionResult Edit(int id)
        //{
        //    var data = db.Patients.First(x => x.PatientId == id);
        //    return View(new PatientEditModel
        //    {
        //        PatientId = data.PatientId,
        //        PatientName = data.PatientName,
        //        Address = data.Address,
        //        Email = data.Email,
        //        Phone = data.Phone,
        //        AdmitDate = data.AdmitDate,
        //        EndDate = data.EndDate,
        //        PaymentBill = data.PaymentBill,
        //        IsAvailable = data.IsAvailable,
        //        CurrentPicture = data.Picture

        //    });
        //}
        //[HttpPost]
        //public ActionResult Edit(PatientEditModel data)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var w = db.Patients.First(x => x.PatientId == data.PatientId);
        //        w.PatientName = data.PatientName;
        //        w.Address = data.Address;
        //        w.Email = data.Email;
        //        w.Phone = data.Phone;
        //        w.AdmitDate = data.AdmitDate;
        //        w.EndDate = data.EndDate;
        //        w.PaymentBill = data.PaymentBill;
        //        w.IsAvailable = data.IsAvailable;
        //        w.Picture = data.CurrentPicture;

        //        if (data.Picture != null)
        //        {
        //            if (data.Picture.ContentLength > 0)
        //            {
        //                string ext = Path.GetExtension(data.Picture.FileName);
        //                string fileName = Guid.NewGuid() + ext;
        //                string savePath = Path.Combine(Server.MapPath("~/Assets"), fileName);
        //                data.Picture.SaveAs(savePath);
        //                w.Picture = fileName;
        //            }
        //        }
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(data);
        //}
    }
}
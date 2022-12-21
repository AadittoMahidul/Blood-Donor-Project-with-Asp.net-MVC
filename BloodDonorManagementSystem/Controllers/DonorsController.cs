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
    public class DonorsController : Controller
    {
        BloodDonorDbContext db = new BloodDonorDbContext();
        // GET: Donors
        public ActionResult Index()
        {
            return View(db.Donors.Include(x => x.Blood).ToList());
        }
        public ActionResult Create()
        {
            ViewBag.Bloods = db.Bloods.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(DonorInputModel c)
        {
            if (ModelState.IsValid)
            {
                var Donor = new Donor
                {
                    DonorName = c.DonorName,
                    Address = c.Address,
                    Email = c.Email,
                    Phone = c.Phone,
                    DonationDate = c.DonationDate,
                    IsAvailable = c.IsAvailable,
                    BloodId = c.BloodId
                };
                string ext = Path.GetExtension(c.Picture.FileName);
                string f = Guid.NewGuid() + ext;
                c.Picture.SaveAs(Server.MapPath("~/Assets/") + f);
                Donor.Picture = f;
                db.Donors.Add(Donor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Bloods = db.Bloods.ToList();
            return View(c);
        }
        public ActionResult Edit(int id)
        {
            var t = db.Donors.First(x => x.DonorId == id);
            ViewBag.Bloods = db.Bloods.ToList();
            ViewBag.CurrentPic = t.Picture;
            return View(new DonorEditModel { DonorId = t.DonorId, DonorName = t.DonorName, Address = t.Address, Email = t.Email, Phone = t.Phone, DonationDate=t.DonationDate, BloodId = t.BloodId/*, Picture = t.Picture*/ });
        }
        [HttpPost]
        public ActionResult Edit(DonorEditModel t)
        {
            var Donor = db.Donors.First(x => x.DonorId == t.DonorId);
            if (ModelState.IsValid)
            {

                Donor.DonorName = t.DonorName;
                Donor.Address = t.Address;
                Donor.Email = t.Email;
                Donor.Phone = t.Phone;
                Donor.DonationDate = t.DonationDate;
                Donor.IsAvailable = t.IsAvailable;
                if (t.Picture != null)
                {
                    string ext = Path.GetExtension(t.Picture.FileName);
                    string f = Guid.NewGuid() + ext;
                    t.Picture.SaveAs(Server.MapPath("~/Assets/") + f);
                    Donor.Picture = f;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentPic = Donor.Picture;
            ViewBag.Bloods = db.Bloods.ToList();
            return View(t);
        }
        public ActionResult Delete(int id)
        {
            return View(db.Donors.Include(x => x.Blood).First(x => x.DonorId == id));
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Donor t = new Donor { DonorId = id };
            db.Entry(t).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
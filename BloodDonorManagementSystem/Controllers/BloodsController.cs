using BloodDonorManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace BloodDonorManagementSystem.Controllers
{
    public class BloodsController : Controller
    {
        BloodDonorDbContext db = new BloodDonorDbContext();
        // GET: Bloods
        public ActionResult Index()
        {
            return View(db.Bloods.Include(b => b.Donors).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        public PartialViewResult CreateBlood()
        {
            return PartialView("_CreateBlood");
        }
        [HttpPost]
        public PartialViewResult CreateBlood(Blood b)
        {
            Thread.Sleep(2500);
            if (ModelState.IsValid)
            {
                db.Bloods.Add(b);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public PartialViewResult EditBlood(int id)
        {
            var b = db.Bloods.First(x => x.BloodId == id);
            return PartialView("_EditBlood", b);
        }
        [HttpPost]
        public PartialViewResult EditBlood(Blood b)
        {
            Thread.Sleep(2500);
            if (ModelState.IsValid)
            {
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Delete(int id)
        {
            return View(db.Bloods.First(x => x.BloodId == id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DoDelete(int BloodId)
        {
            var b = new Blood { BloodId = BloodId };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
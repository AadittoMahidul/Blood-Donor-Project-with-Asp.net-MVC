using BloodDonorManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BloodDonorManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        BloodDonorDbContext db = new BloodDonorDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var data = db.Donors.ToList();
            return View();
        }
    }
}
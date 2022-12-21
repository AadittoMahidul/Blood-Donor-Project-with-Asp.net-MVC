using BloodDonorManagementSystem.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Microsoft.Owin.Security;


namespace BloodDonorManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var User_Store = new UserStore<IdentityUser>();

                var User_Manager = new UserManager<IdentityUser>(User_Store);
                var user = new IdentityUser { UserName = model.UserName };
                var result = User_Manager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    var Auth_Manager = HttpContext.GetOwinContext().Authentication;

                    var User_Identity = User_Manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    Auth_Manager.SignIn(new AuthenticationProperties() { }, User_Identity);

                    return RedirectToActionPermanent("Login");
                }
                
            }
            else
            {
                ModelState.AddModelError("", "Register failed");
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var User_Store = new UserStore<IdentityUser>();

                var User_Manager = new UserManager<IdentityUser>(User_Store);

                var user = User_Manager.Find(model.UserName, model.Password);
                if(user != null)
                {
                    var Auth_Manager = HttpContext.GetOwinContext().Authentication;

                    var User_Identity = User_Manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);



                    Auth_Manager.SignIn(new AuthenticationProperties() { IsPersistent = false }, User_Identity);
                    return RedirectToActionPermanent("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login failed");
                }
            }
            return View(model);

        }
        public ActionResult Logout()
        {
            var Auth_Manager = HttpContext.GetOwinContext().Authentication;
            Auth_Manager.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}
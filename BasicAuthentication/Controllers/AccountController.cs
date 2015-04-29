using BasicAuthentication.Infrastructure;
using BasicAuthentication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BasicAuthentication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserManager _usermanager;
        private readonly IUserRepository _userrepository;

        public AccountController(IUserManager manager)
        {
            _usermanager = manager;
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool userValid = _usermanager.AuthenticateCredentials(model.username, model.password);

                if (userValid)
                {
                    FormsAuthentication.SetAuthCookie(model.username, model.remember);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Profile()
        {
            var user = _usermanager.FindUser(User.Identity.Name);

            var model = new UserViewModel()
            {
                username = user.Name
            };

            return View(model);
        }

        public ActionResult UploadForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadProfilePicture(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("error", "El archivo que enviaste es incorrecto");
            }

            string extension = Path.GetExtension(file.FileName);
            if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png"))
            {
                string serverPath = Server.MapPath("~/Content");

                if (ModelState.IsValid)
                {
                    _usermanager.UploadProfileImage(User.Identity.Name, file, serverPath);
                }
            }

            return RedirectToAction("Profile");
        }

        [ChildActionOnly]
        public ActionResult ProfileImage()
        {
            var model = _usermanager.GetProfileImage(User.Identity.Name);

            ViewBag.path = model;

            return PartialView();
        }
    }
}
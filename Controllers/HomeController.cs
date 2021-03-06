using LibraryProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();

            homeIndexViewModel.Information = db.Informations.ToList();
            homeIndexViewModel.Books = db.Books
                .OrderByDescending(x => x.CreationDate)
                .Take(5).ToList();
            return View(homeIndexViewModel);
        }

        public async Task<ActionResult> About()
        {
            //var _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var user = await _userManager.FindByIdAsync(User.Identity.GetUserId());
            //var logins = user.Logins;
            //var rolesForUser = await _userManager.GetRolesAsync(User.Identity.GetUserId());
            //foreach (var login in logins.ToList())
            //{
            //    await _userManager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
            //}

            //if (rolesForUser.Count() > 0)
            //{
            //    foreach (var item in rolesForUser.ToList())
            //    {
            //        // item should be the name of the role
            //        var result = await _userManager.RemoveFromRoleAsync(user.Id, item);
            //    }
            //}

            //await _userManager.DeleteAsync(user);

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
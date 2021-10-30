using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class HomeController : Controller
    {       
        public ActionResult Index()
        {
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();
            List<Information> informationList = new List<Information>();

            Information information1 = new Information()
            {
                Title = "The Crash",
                Content = "We are going to take this damage by the end of the season",
                CreationDate = DateTime.Now
            };

            informationList.Add(information1);
            for(int i=0; i<5; i++)
            {
                Information information = new Information()
                {
                    Title = "The Title " + i.ToString(),
                    Content = "Content of this graphic is truly amazing im tellin ya!",
                    CreationDate = DateTime.Now
                };
                informationList.Add(information);
            }

            homeIndexViewModel.Information = informationList;
            return View(homeIndexViewModel);
        }

        public ActionResult About()
        {
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
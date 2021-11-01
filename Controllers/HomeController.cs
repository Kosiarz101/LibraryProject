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
                Id = 0,
                Title = "The Crash",
                Content = "We are going to take this damage by the end of the season",
                CreationDate = DateTime.Now
            };

            informationList.Add(information1);
            for(int i=0; i<5; i++)
            {
                Information information = new Information()
                {
                    Id = i + 1,
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
            List<Book> bookModels = new List<Book>();
            for (int i = 0; i < 6; i++)
            {
                Book book = new Book()
                {
                    BookId = i,
                    Title = "tytul" + i.ToString(),
                    Author = "Thomas James",
                    ISPNNumber = 1234 + i,
                    CreationDate = DateTime.Now,
                    PublicationDate = DateTime.Now
                };
                bookModels.Add(book);
            }
            Session["books"] = bookModels;
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
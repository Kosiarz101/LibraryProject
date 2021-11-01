using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class InformationController : Controller
    {
        // GET: Information
        public ActionResult Index()
        {
            return View();
        }

        // GET: Information/Details/5
        public ActionResult Details(int id)
        {
            List<Information> informationList = new List<Information>();

            Information information1 = new Information()
            {
                Title = "The Crash",
                Content = "We are going to take this damage by the end of the season",
                CreationDate = DateTime.Now
            };

            informationList.Add(information1);
            for (int i = 0; i < 5; i++)
            {
                Information information = new Information()
                {
                    Title = "The Title " + i.ToString(),
                    Content = "Content of this graphic is truly amazing im tellin ya!",
                    CreationDate = DateTime.Now
                };
                informationList.Add(information);
            }
            return View(informationList[id]);
        }

        // GET: Information/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Information/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Information/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Information/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Information/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Information/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

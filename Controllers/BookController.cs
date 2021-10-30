using LibraryProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchBooks(string input, string selectedValue)
        {
            List<Book> bookModels = new List<Book>();
            for (int i=0; i<6; i++)
            {
                Book book = new Book()
                {
                    Title = "tytul" + i.ToString(),
                    Author = "Thomas James",
                    ISPNNumber = 1234 + i,
                    CreationDate = DateTime.Now
                };
                bookModels.Add(book);
            }
            if (input == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (selectedValue == "title")
            {
                bookModels = bookModels
                             .Where(x => x.Title == input)
                             .ToList();
            }
            else if (selectedValue == "isbn")
            {
                if (Int32.TryParse(input, out int result))
                {
                    bookModels = bookModels
                    .Where(x => x.ISPNNumber == result)
                    .ToList();
                }
                else
                {
                    View(bookModels);
                }

            }
            else
            {
                bookModels = bookModels
                             .Where(x => x.Author == input)
                             .ToList();
            }
            return View(bookModels);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
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

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Book/Edit/5
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

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Book/Delete/5
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

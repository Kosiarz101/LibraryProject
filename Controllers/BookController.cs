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
        List<Book> bookModels = new List<Book>();
        // GET: Book
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchBooks(string input, string selectedValue)
        {
            //for (int i=0; i<6; i++)
            //{
            //    Book book = new Book()
            //    {
            //        BookId = i,
            //        Title = "tytul" + i.ToString(),
            //        Author = "Thomas James",
            //        ISPNNumber = 1234 + i,
            //        CreationDate = DateTime.Now
            //    };
            //    bookModels.Add(book);
            //}
            bookModels = (List<Book>)Session["books"];
            if (input == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (selectedValue == "title")
            {
                bookModels = bookModels
                             .Where(x => x.Title.Contains(input))
                             .ToList();
            }
            else if (selectedValue == "isbn")
            {
                if (Int32.TryParse(input, out int result))
                {
                    bookModels = bookModels
                    .Where(x => x.ISPNNumber.ToString().Contains(input))
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
                             .Where(x => x.Author.Contains(input))
                             .ToList();
            }
            return View(bookModels);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            //for (int i = 0; i < 6; i++)
            //{
            //    Book book = new Book()
            //    {
            //        BookId = i,
            //        Title = "tytul" + i.ToString(),
            //        Author = "Thomas James",
            //        ISPNNumber = 1234 + i,
            //        CreationDate = DateTime.Now
            //    };
            //    bookModels.Add(book);
            //}
            bookModels = (List<Book>)Session["books"];
            return View(bookModels[id]);
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
            //for (int i = 0; i < 6; i++)
            //{
            //    Book book = new Book()
            //    {
            //        BookId = i,
            //        Title = "tytul" + i.ToString(),
            //        Author = "Thomas James",
            //        ISPNNumber = 1234 + i,
            //        CreationDate = DateTime.Now
            //    };
            //    bookModels.Add(book);
            //}
            bookModels = (List<Book>)Session["books"];
            return View(bookModels[id]);
        }

        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Book collection)
        {
            try
            {
                bookModels = (List<Book>)Session["books"];
                bookModels[id] = collection;
                Session["books"] = bookModels;

                return RedirectToAction("Index", "Home");
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

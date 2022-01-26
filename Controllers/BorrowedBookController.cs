using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Models;

namespace LibraryProject.Controllers
{
    public class BorrowedBookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BorrowedBook
        public ActionResult Index()
        {
            var borrowedBooks = db.BorrowedBooks.Include(b => b.ApplicationUser).Include(b => b.Book);
            return View(borrowedBooks.ToList());
        }

        //GET: BorrowedBook/BorrowedBookList
        public ActionResult BorrowedBookList(string UserId, int? BookId)
        {
            if (UserId == null && BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            else if (UserId == null)
            {

            }
            else if (BookId == null)
            {
                string bookLimit = db.GlobalParameters.Where(x => x.Name.ToLower() == "book limit").FirstOrDefault().Value;
                ViewData["booklimit"] = bookLimit;
                List<BorrowedBook> borrowedBooks = db.BorrowedBooks
                                     .Include(x => x.Book)
                                     .Where(x => x.ApplicationUserId == UserId)
                                     .ToList();
                int borrowedBooksCount = db.AwaitedBooks.Where(x => x.ApplicationUserId == UserId).Count();
                ViewData["booksleft"] = Int32.Parse(bookLimit) - (borrowedBooks.Count + borrowedBooksCount);
                return View("BorrowedBookListByUser", borrowedBooks);
            }
            return View();
        }
        // GET: BorrowedBook/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowedBook borrowedBook = db.BorrowedBooks.Find(id);
            if (borrowedBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowedBook);
        }

        // GET: BorrowedBook/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title");
            return View();
        }

        // POST: BorrowedBook/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationUserId,BookId,CreationDate")] BorrowedBook borrowedBook)
        {
            if (ModelState.IsValid)
            {
                db.BorrowedBooks.Add(borrowedBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", borrowedBook.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", borrowedBook.BookId);
            return View(borrowedBook);
        }

        // POST: BorrowedBook/CreateAjax
        [HttpPost]
        public ActionResult CreateAjax(string UserId, int? BookId)
        {
            if (UserId == null || BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ////bool isInAQueue = db.Queues.Any(x => x.ApplicationUserId == UserId && x.BookId == BookId);
            //if (isInAQueue)
            //{
            //    ViewData["message"] = "You are already in a queue for this book";
            //    ViewData["messageType"] = "Info";
            //    return PartialView("_MessageAlert");
            //}
            BorrowedBook borrowedBook = new BorrowedBook()
            {
                ApplicationUserId = UserId,
                BookId = (int)BookId,
                CreationDate = DateTime.Now
            };
            db.BorrowedBooks.Add(borrowedBook);
            AwaitedBook awaitedBook = db.AwaitedBooks.Find(UserId, BookId);
            if(awaitedBook != null)
                db.AwaitedBooks.Remove(awaitedBook);
            db.SaveChanges();

            //ViewData["message"] = "Book has been added to users library!";
            //ViewData["messageType"] = "Success";
            return RedirectToAction("UserPage", "Manage", new { id = UserId});
        }

        // GET: BorrowedBook/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowedBook borrowedBook = db.BorrowedBooks.Find(id);
            if (borrowedBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", borrowedBook.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", borrowedBook.BookId);
            return View(borrowedBook);
        }

        // POST: BorrowedBook/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationUserId,BookId,CreationDate")] BorrowedBook borrowedBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowedBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", borrowedBook.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", borrowedBook.BookId);
            return View(borrowedBook);
        }

        // GET: BorrowedBook/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowedBook borrowedBook = db.BorrowedBooks.Find(id);
            if (borrowedBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowedBook);
        }

        // POST: BorrowedBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BorrowedBook borrowedBook = db.BorrowedBooks.Find(id);
            db.BorrowedBooks.Remove(borrowedBook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

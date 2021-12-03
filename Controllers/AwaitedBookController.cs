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
    public class AwaitedBookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AwaitedBook
        public ActionResult Index()
        {
            var awaitedBooks = db.AwaitedBooks.Include(a => a.ApplicationUser).Include(a => a.Book);
            return View(awaitedBooks.ToList());
        }

        //GET: QueueList
        public ActionResult AwaitedBookList(string UserId, int? BookId)
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
                List<AwaitedBook> queues = db.AwaitedBooks
                                     .Include(x => x.Book)
                                     .Where(x => x.ApplicationUserId == UserId)
                                     .ToList();
                return View("AwaitedBookListByUser", queues);
            }
            return View();
        }

        // GET: AwaitedBook/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwaitedBook awaitedBook = db.AwaitedBooks.Find(id);
            if (awaitedBook == null)
            {
                return HttpNotFound();
            }
            return View(awaitedBook);
        }

        // GET: AwaitedBook/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title");
            return View();
        }

        // POST: AwaitedBook/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationUserId,BookId,CreationDate")] AwaitedBook awaitedBook)
        {
            if (ModelState.IsValid)
            {
                db.AwaitedBooks.Add(awaitedBook);
                Book book = db.Books.Find(awaitedBook.BookId);
                book.Quantity--;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", awaitedBook.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", awaitedBook.BookId);
            return View(awaitedBook);
        }

        // GET: AwaitedBook/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwaitedBook awaitedBook = db.AwaitedBooks.Find(id);
            if (awaitedBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", awaitedBook.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", awaitedBook.BookId);
            return View(awaitedBook);
        }

        // POST: AwaitedBook/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationUserId,BookId,CreationDate")] AwaitedBook awaitedBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(awaitedBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", awaitedBook.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", awaitedBook.BookId);
            return View(awaitedBook);
        }

        // GET: AwaitedBook/Delete/5
        public ActionResult Delete(string UserId, int? BookId)
        {
            if (UserId == null || BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AwaitedBook awaitedBook = db.AwaitedBooks.Find(UserId, BookId);
            if (awaitedBook == null)
            {
                return HttpNotFound();
            }
            return View(awaitedBook);
        }

        // POST: AwaitedBook/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string UserId, int? BookId)
        {
            AwaitedBook awaitedBook = db.AwaitedBooks.Find(UserId, BookId);
            Book book = db.Books.Find(BookId);
            book.Quantity++;
            db.AwaitedBooks.Remove(awaitedBook);
            db.ChangePlaceInQuery(book);
            db.SaveChanges();
            return RedirectToAction("../Manage/Index");
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

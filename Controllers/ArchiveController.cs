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
    public class ArchiveController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Archive
        public ActionResult Index()
        {
            var archives = db.Archives.Include(a => a.ApplicationUser).Include(a => a.Book);
            return View(archives.ToList());
        }

        //GET: BorrowedBook/ArchiveList
        public ActionResult ArchiveList(string UserId, int? BookId)
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
                List<Archive> archive = db.Archives
                                     .Include(x => x.Book)
                                     .Where(x => x.ApplicationUserId == UserId)
                                     .OrderByDescending(x => x.CreationDate)
                                     .ToList();
                return View("ArchiveListByUser", archive);
            }
            return View();
        }

        // POST: Archive/CreateFromUserPage
        [HttpPost]
        public ActionResult CreateFromUserPage(string UserId, int? BookId)
        {
            if (UserId == null || BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Archive archive = new Archive()
            {
                ApplicationUserId = UserId,
                BookId = (int)BookId,
                CreationDate = DateTime.Now
            };
            BorrowedBook borrowedBook = db.BorrowedBooks.Find(UserId, BookId);           
            if (borrowedBook == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            db.BorrowedBooks.Remove(borrowedBook);
            db.Archives.Add(archive);

            Book book = db.Books.Find(BookId);
            book.Quantity++;
            db.ChangePlaceInQuery(book);
            db.SaveChanges();

            return RedirectToAction("UserPage", "Manage", new { id = UserId });
        }

        // GET: Archive/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = db.Archives.Find(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            return View(archive);
        }

        // GET: Archive/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title");
            return View();
        }

        // POST: Archive/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationUserId,BookId,CreationDate")] Archive archive)
        {
            if (ModelState.IsValid)
            {
                db.Archives.Add(archive);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", archive.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", archive.BookId);
            return View(archive);
        }

        // GET: Archive/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = db.Archives.Find(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", archive.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", archive.BookId);
            return View(archive);
        }

        // POST: Archive/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationUserId,BookId,CreationDate")] Archive archive)
        {
            if (ModelState.IsValid)
            {
                db.Entry(archive).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", archive.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", archive.BookId);
            return View(archive);
        }

        // GET: Archive/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Archive archive = db.Archives.Find(id);
            if (archive == null)
            {
                return HttpNotFound();
            }
            return View(archive);
        }

        // POST: Archive/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Archive archive = db.Archives.Find(id);
            db.Archives.Remove(archive);
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

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
    public class BookTagController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BookTag
        public ActionResult Index()
        {
            var bookTags = db.BookTags.Include(b => b.Book).Include(b => b.Tag);
            return View(bookTags.ToList());
        }

        // GET: BookTag/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTag bookTag = db.BookTags.Find(id);
            if (bookTag == null)
            {
                return HttpNotFound();
            }
            return View(bookTag);
        }

        // GET: BookTag/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["TagName"] = new SelectList(db.Tags, "Name", "Name");
            if(!db.Books.Any(x => x.Id == id))
            {
                return HttpNotFound();
            }
            ViewData["BookId"] = id;
            return View();
        }

        // POST: BookTag/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,TagName")] BookTag bookTag)
        {
            if(db.BookTags.Any(x => x.BookId == bookTag.BookId && x.TagName == bookTag.TagName))
            {
                ModelState.AddModelError("Duplicate", "This tag is already asigned to this book");
            }
            if (ModelState.IsValid)
            {
                db.BookTags.Add(bookTag);
                db.SaveChanges();
                return RedirectToAction("BookPage", "Book", new { id = bookTag.BookId});
            }

            ViewData["BookId"] = bookTag.BookId;
            ViewData["TagName"] = new SelectList(db.Tags, "Name", "Name", bookTag.TagName);
            return View(bookTag);
        }

        // GET: BookTag/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookTag bookTag = db.BookTags.Find(id);
            if (bookTag == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", bookTag.BookId);
            ViewBag.TagName = new SelectList(db.Tags, "Name", "Name", bookTag.TagName);
            return View(bookTag);
        }

        // POST: BookTag/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,TagName")] BookTag bookTag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookTag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", bookTag.BookId);
            ViewBag.TagName = new SelectList(db.Tags, "Name", "Name", bookTag.TagName);
            return View(bookTag);
        }

        // GET: BookTag/Delete/5
        public ActionResult Delete(int? BookId)
        {
            if (BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Tag> tags = db.BookTags
                             .Where(x => x.BookId == BookId)
                             .Select(x => x.Tag)
                             .ToList();
            ViewData["TagName"] = new SelectList(tags, "Name", "Name");
            if (!db.Books.Any(x => x.Id == BookId))
            {
                return HttpNotFound();
            }
            ViewData["BookId"] = BookId;
            return View();
        }

        // POST: BookTag/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "BookId,TagName")] BookTag thisBookTag)
        {
            BookTag bookTag = db.BookTags.Find(thisBookTag.BookId, thisBookTag.TagName);
            db.BookTags.Remove(bookTag);
            db.SaveChanges();
            return RedirectToAction("BookPage", "Book", new { Id = thisBookTag.BookId });
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

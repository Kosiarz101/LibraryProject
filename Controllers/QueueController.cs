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
    public class QueueController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Queue
        public ActionResult Index()
        {
            var queues = db.Queues.Include(q => q.ApplicationUser).Include(q => q.Book);
            return View(queues.ToList());
        }
        //GET: QueueList
        public ActionResult QueueList(string UserId, int? BookId)
        {
            if (UserId == null && BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            else if(UserId == null)
            {
                
            }
            else if(BookId == null)
            {
                List<Queue> queues = db.Queues
                                     .Include(x => x.Book)
                                     .Where(x => x.ApplicationUserId == UserId)
                                     .ToList();
                return View("QueueListByUser", queues);
            }
            return View();
        }

        // GET: Queue/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queue queue = db.Queues.Find(id);
            if (queue == null)
            {
                return HttpNotFound();
            }
            return View(queue);
        }

        // GET: Queue/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email");
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title");
            return View();
        }

        // POST: Queue/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationUserId,BookId")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                db.Queues.Add(queue);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", queue.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", queue.BookId);
            return View(queue);
        }
        [HttpPost]
        public ActionResult CreateAjax(string UserId, int? BookId)
        {
            if (UserId == null || BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool isInAQueue = db.Queues.Any(x => x.ApplicationUserId == UserId && x.BookId == BookId);
            if(isInAQueue)
            {
                ViewData["message"] = "You are already in a queue for this book";
                ViewData["messageType"] = "Info";
                return PartialView("_MessageAlert");
            }
            List<Queue> queues = db.Queues.Where(x => x.BookId == BookId).ToList();
            Queue queue = new Queue()
            {
                ApplicationUserId = UserId,
                BookId = (int)BookId,
                CreationDate = DateTime.Now,
                Place = queues.Count + 1
            };                 
            db.Queues.Add(queue);
            db.SaveChanges();
           
            ViewData["message"] = "You have been successfully added to queue! Your place in the queue is: " + queue.Place;
            ViewData["messageType"] = "Info";
            return PartialView("_MessageAlert");
        }

        // GET: Queue/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queue queue = db.Queues.Find(id);
            if (queue == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", queue.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", queue.BookId);
            return View(queue);
        }

        // POST: Queue/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationUserId,BookId")] Queue queue)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queue).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "Email", queue.ApplicationUserId);
            ViewBag.BookId = new SelectList(db.Books, "Id", "Title", queue.BookId);
            return View(queue);
        }

        // GET: Queue/Delete/5
        public ActionResult Delete(string UserId, int? BookId)
        {
            if (UserId == null || BookId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Queue queue = db.Queues.Find(UserId, BookId);
            if (queue == null)
            {
                return HttpNotFound();
            }
            return View(queue);
        }

        // POST: Queue/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string UserId, int? BookId)
        {
            Queue queue = db.Queues.Find(UserId, BookId); 
            List<Queue> queues = db.Queues
                                 .Where(x => x.BookId == BookId)
                                 .OrderBy(x => x.Place)
                                 .ToList();
            int position = queues.FindIndex(x => x.ApplicationUserId == UserId);
            if(queues.Count > 1)
            {
                for (int i = position + 1; i < queues.Count; i++)
                {
                    queues[i].Place -= 1;
                }
            }        
            db.Queues.Remove(queue);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LibraryProject.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace LibraryProject.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult SearchBooks(string input, string selectedValue)
        {
            List<Book> bookModels = new List<Book>(); 
            if (input == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (selectedValue == "title")
            {
                bookModels = db.Books
                             .Where(x => x.Title.Contains(input))
                             .ToList();
            }
            else if (selectedValue == "isbn")
            {
                bookModels = db.Books
                            .Where(x => x.ISPNNumber.ToString().Contains(input))
                            .ToList();
            }
            else
            {
                bookModels = db.Books
                             .Where(x => x.Author.Contains(input))
                             .ToList();
            }
            return View(bookModels);
        }
        // GET: ShoppingCart
        public ActionResult ShoppingCart()
        {
            List<Book> books;
            if (Session["shoppingcart"] is List<Book>)
            {
                books = (List<Book>)Session["shoppingcart"];
            }
            else
            {
                books = new List<Book>();
            }             
            return View(books);
        }
        // POST: ShoppingCart delete book
        [HttpPost]
        [ActionName("ShoppingCartDelete")]
        public ActionResult ShoppingCart(int? id)
        {
            List<Book> books = new List<Book>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Session["shoppingcart"] is List<Book>)
            {
                books = (List<Book>)Session["shoppingcart"];
                books = books.Where(x => x.Id != id).ToList();
                Session["shoppingcart"] = books;
            }
            return View("ShoppingCart", books);
        }
        // POST: ShoppingCart finalise transaction 
        [HttpPost]
        [ActionName("ShoppingCartFinalise")]
        public ActionResult ShoppingCart(IList<Book> books)
        {
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            ApplicationUser applicationUser = userManager.FindById(User.Identity.GetUserId());
            List<int> ids = new List<int>();
            for(int i=0; i<books.Count; i++)
            {
                ids.Add(books[i].Id);
            }

            //Get all books that current user is awaiting
            List<AwaitedBook> awaitedBooks = db.AwaitedBooks
                                               .Include(x => x.ApplicationUser)
                                               .Where(x => x.ApplicationUserId == applicationUser.Id)
                                               .Include(x => x.Book)
                                               .Where(x => ids.Contains(x.BookId))
                                               .ToList();
            if(awaitedBooks.Count > 0)
            {
                ViewData["message"] = "You have already borrowed one of a book from a cart";
                ViewData["messageType"] = "info";
                return View("ShoppingCart", books);
            }
            foreach(var item in books)
            {
                AwaitedBook awaitedBook = new AwaitedBook()
                {
                    ApplicationUserId = applicationUser.Id,
                    BookId = item.Id
                };
                db.AwaitedBooks.Add(awaitedBook);
            }
            for (int i = 0; i < books.Count(); i++)
            {
                books[i].Quantity--;
                db.Entry(books[i]).State = EntityState.Modified;
            }
            db.SaveChanges();
            Session["shoppingcart"] = null;
            books = new List<Book>();
            return RedirectToAction("ShoppingCart", "Book", books);
        }
        public PartialViewResult AddToCart(int? id)
        {
            List<Book> books = new List<Book>();
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                //return HttpNotFound();
            }
            if (Session["shoppingcart"] is List<Book>)
            {
                books = (List<Book>)Session["shoppingcart"];               
            }
            if (!books.Exists(x => x.Id == id))
                books.Add(book);
            Session["shoppingcart"] = books;
            ViewData["message"] = "You added book to your cart!";
            ViewData["messageType"] = "Info";
            return PartialView("_MessageAlert");
        }
        // GET: Book
        public ActionResult Index()
        {
            return View(db.Books.ToList());
        }

        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        //GET: Book/BookPage/5
        public ActionResult BookPage(int? id)
        {
            BookPageViewModel bookPageViewModel = new BookPageViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            bookPageViewModel.Book = book;
            //W tabeli typu joint znajdź wszystkie rekordy z odpowiednim id książki a potem dołącz wszystkie tagi
            List<Tag> tags = db.BookTags.Where(x => x.BookId == book.Id).Select(x => x.Tag).ToList();
            bookPageViewModel.Tags = tags;
            return View(bookPageViewModel);
        }
        // GET: Book/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Book/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Describtion,ISPNNumber,Author,PublicationDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                book.CreationDate = DateTime.Now;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Describtion,ISPNNumber,Author,CreationDate,PublicationDate,Quantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

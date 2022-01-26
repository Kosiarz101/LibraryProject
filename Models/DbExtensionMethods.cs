using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryProject.Models
{
    public static class DbExtensionMethods
    {
        public static void ChangePlaceInQuery(this ApplicationDbContext db, ApplicationUserManager userManager, Book book)
        {
            //Get all queues for particular book
            List<Queue> queues = db.Queues
                                 .Where(x => x.BookId == book.Id)
                                 .OrderBy(x => x.Place)
                                 .ToList();
            //Return if there is no queue
            if (queues.Count == 0)
                return;
            //Move everyone in a queue forward
            for (int i = 0; i < queues.Count; i++)
            {
                queues[i].Place -= 1;
            }
            //Add to first user in a queue awaited book
            AwaitedBook awaitedBook = new AwaitedBook()
            {
                ApplicationUserId = queues[0].ApplicationUserId,
                BookId = book.Id,
                CreationDate = DateTime.Now
            };
            db.AwaitedBooks.Add(awaitedBook);
            //Remove this user from a queue
            db.Queues.Remove(queues[0]);
            book.Quantity--;
            //Send email informing this user about getting his book
            var user = userManager.FindByIdAsync(awaitedBook.ApplicationUserId).Result;
            if(user.EmailConfirmed)
            {
                userManager.SendEmailAsync(user.Id,
                                       "Book " + book.Title + " is now available",
                                       "Book <b>" + book.Title + "</b> is now available and has been moved to your account. Book is awaiting for you in library.");
            }
            
        }
    }
}
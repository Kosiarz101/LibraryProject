using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryProject.Models
{
    public static class DbExtensionMethods
    {
        public static void ChangePlaceInQuery(this ApplicationDbContext db, Book book)
        {
            List<Queue> queues = db.Queues
                                 .Where(x => x.BookId == book.Id)
                                 .OrderBy(x => x.Place)
                                 .ToList();
            if (queues.Count == 0)
                return;
            for (int i = 0; i < queues.Count; i++)
            {
                queues[i].Place -= 1;
            }
            AwaitedBook awaitedBook = new AwaitedBook()
            {
                ApplicationUserId = queues[0].ApplicationUserId,
                BookId = book.Id,
                CreationDate = DateTime.Now
            };
            db.AwaitedBooks.Add(awaitedBook);
            db.Queues.Remove(queues[0]);
            book.Quantity--;
        }
    }
}
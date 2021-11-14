namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LibraryProject.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "LibraryProject.Models.ApplicationDbContext";
        }

        protected override void Seed(LibraryProject.Models.ApplicationDbContext context)
        {
            if(!context.Books.Any(x => x.Title == "Czarodziej Patryk"))
            {
                context.Books.AddOrUpdate(x => x.Id,
                new Models.Book()
                {
                    Title = "Czarodziej Patryk",
                    ISPNNumber = "1123485582367",
                    Author = "Patryk Wrona",
                    PublicationDate = DateTime.Parse("2010-06-11"),
                    Describtion = "Patryk wyrusza do szko³y czarodziejów zbieraæ ¿o³êdzie. Poznaj jego stronê historii.",
                    CreationDate = DateTime.Now,
                    Quantity = 2
                });
            }
            if (!context.Books.Any(x => x.Title == "Fioletowy"))
            {
                context.Books.AddOrUpdate(x => x.Id,
                new Models.Book()
                {
                    Title = "Fioletowy",
                    ISPNNumber = "1123485584367",
                    Author = "Robert Kreska",
                    PublicationDate = DateTime.Parse("2015-06-15"),
                    Describtion = "Malarz postanawia pewnego dnia pomalowaæ swoj¹ teœciow¹ na fioletowo. Tylko dla ludzi o mocnych nerwach",
                    CreationDate = DateTime.Now,
                    Quantity = 2
                });
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}

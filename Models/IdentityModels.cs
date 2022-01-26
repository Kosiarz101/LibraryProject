using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LibraryProject.Models
{
    // Możesz dodać dane profilu dla użytkownika, dodając więcej właściwości do klasy ApplicationUser. Odwiedź stronę https://go.microsoft.com/fwlink/?LinkID=317594, aby dowiedzieć się więcej.
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Search> Searches { get; set; }
        public virtual ICollection<AwaitedBook> AwaitedBooks { get; set; }
        public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
        public virtual ICollection<Archive> Archives { get; set; }
        public virtual ICollection<Queue> Queues { get; set; }
        [Display(Name = "Search Save Mode")]
        public bool isSearchSaveModeActivated { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Element authenticationType musi pasować do elementu zdefiniowanego w elemencie CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Dodaj tutaj niestandardowe oświadczenia użytkownika
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<AwaitedBook> AwaitedBooks { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Archive> Archives { get; set; }
        public DbSet<Queue> Queues { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AwaitedBook>().HasKey(sc => new { sc.ApplicationUserId, sc.BookId });
            modelBuilder.Entity<Queue>().HasKey(sc => new { sc.ApplicationUserId, sc.BookId });
            modelBuilder.Entity<BorrowedBook>().HasKey(sc => new { sc.ApplicationUserId, sc.BookId });
            modelBuilder.Entity<Archive>()
                .HasRequired<Book>(x => x.Book)
                .WithMany(x => x.Archives)
                .HasForeignKey(x => x.BookId);
            modelBuilder.Entity<Archive>()
                 .HasRequired<ApplicationUser>(x => x.ApplicationUser)
                 .WithMany(x => x.Archives)
                 .HasForeignKey(x => x.ApplicationUserId);
            modelBuilder.Entity<BookTag>().HasKey(sc => new { sc.BookId, sc.TagName });
        }
    }
}
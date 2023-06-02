using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp
{
    internal class BooksDbContext : DbContext
    {
        public DbSet<BookAndBookstore> BooksAndBookstores { get; set; }

        public BooksDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseInMemoryDatabase("BooksDb");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasOne(bk => bk.Author)
                .WithMany(au => au.Books)
                .HasForeignKey(bk => bk.AuthorId);

            modelBuilder.Entity<BookAndBookstore>()
                .HasOne(babs => babs.Book)
                .WithMany(bk => bk.BooksAndBookstores)
                .HasForeignKey(babs => babs.BookId);

            modelBuilder.Entity<BookAndBookstore>()
                .HasOne(babs => babs.Bookstore)
                .WithMany(bs => bs.BooksAndBookstores)
                .HasForeignKey(babs => babs.BookstoreId);

            // Seed data for Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "HG Wells" },
                new Author { Id = 2, Name = "W Golding" },
                new Author { Id = 3, Name = "EA Poe" },
                new Author { Id = 4, Name = "I Asimov" }
            );

            // Seed data for Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Invisible Man", AuthorId = 1, Year = 1897 },
                new Book { Id = 2, Title = "The Time Machine", AuthorId = 1, Year = 1895 },
                new Book { Id = 3, Title = "Lord of the Flies", AuthorId = 2, Year = 1954 },
                new Book { Id = 4, Title = "The Tell-Tale Heart", AuthorId = 3, Year = 2002 },
                new Book { Id = 5, Title = "The Raven and Other Poems", AuthorId = 3, Year = 2006 },
                new Book { Id = 6, Title = "The Foundation", AuthorId = 4, Year = 1942 }
            );

            // Seed data for Bookstores
            modelBuilder.Entity<Bookstore>().HasData(
                new Bookstore { Id = 1, Name = "Libri" },
                new Bookstore { Id = 2, Name = "Alexandra" },
                new Bookstore { Id = 3, Name = "Bookline" }
            );

            // Seed data for BookAndBookstore table
            modelBuilder.Entity<BookAndBookstore>().HasData(
                new BookAndBookstore { Id = 1, BookId = 1, BookstoreId = 1 }, // Invisible Man a Libriben
                new BookAndBookstore { Id = 2, BookId = 1, BookstoreId = 2 }, // Invisible Man az Alexandraban
                new BookAndBookstore { Id = 3, BookId = 2, BookstoreId = 2 }, // The Time Machine az Alexandraban
                new BookAndBookstore { Id = 4, BookId = 3, BookstoreId = 3 }, // Lord of the Flies a Bookline-on
                new BookAndBookstore { Id = 5, BookId = 4, BookstoreId = 1 }, // The Tell-Tale Heart a Libriben
                new BookAndBookstore { Id = 6, BookId = 4, BookstoreId = 2 } // The Tell-Tale Heart az Alexandraban
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}

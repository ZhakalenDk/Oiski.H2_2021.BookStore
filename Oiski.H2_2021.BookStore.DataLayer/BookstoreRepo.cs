using Microsoft.EntityFrameworkCore;
using Oiski.H2_2021.BookStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oiski.H2_2021.BookStore.DataLayer
{
    public class BookstoreRepo
    {
        private BookstoreRepo() { } //  To ensure no instantiation from outside this scope

        private static BookstoreRepo access = null;
        public static BookstoreRepo Access
        {
            get
            {
                access ??= new BookstoreRepo();

                return access;
            }
        }

        public T GetByID<T>(int _id)
        {
            T obj = default;
            using (var context = new BookstoreContext())
            {
                obj = ( T )context.Find(typeof(T), _id);
            }

            return obj;
        }

        /// <summary>
        /// Finds all <see cref="Book"/> entities in DB and searches all related auther tables for attached data
        /// </summary>
        /// <returns>A <see cref="Book"/> collection with related auther data included</returns>
        public ICollection<Book> GetBooksAuthersIncluded()
        {
            ICollection<Book> books = null;
            using (var context = new BookstoreContext())
            {
                books = context.Books
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .ToList();
            }

            return books;
        }

        public Book GetFirstBook()
        {
            using (var context = new BookstoreContext())
            {
                return context.Books
                    .ToList()
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Eager Loading because all related data is fetched in the initial query
        /// </summary>
        /// <returns></returns>
        public Book GetFirstBookIncluded()
        {
            Book book = null;
            using (var context = new BookstoreContext())
            {
                book = context.Books
                    .Include(b => b.Reviews)
                    .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                    .ToList()
                    .FirstOrDefault();
            }

            return book;
        }

        public void AddBookWithNewReview()
        {
            using (var context = new BookstoreContext())
            {
                Book book = new Book
                {
                    Title = "Test Book",
                    PublishedOn = DateTime.Now,

                    Reviews = new List<Review>() { new Review() { NumStars = 5, Comment = "Great Test Book", VoterName = "MR. U Test" } }
                };

                context.Books.Add(book);

                context.SaveChanges();
            }
        }

        public void AddBookWithOldAuthor()
        {
            using (var context = new BookstoreContext())
            {
                Book oldBook = context.Books.Where(b => b.BookID == 2)
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                    .Single();

                Book newBook = new Book()
                {
                    Title = "Test Book 2",
                    PublishedOn = DateTime.Now,
                    BookAuthors = new List<BookAuthor>()
                };

                var author = oldBook.BookAuthors.First().Author;

                newBook.BookAuthors.Add(new BookAuthor() { Book = newBook, Author = author });

                context.Add(newBook);

                context.SaveChanges();

            }
        }
    }
}

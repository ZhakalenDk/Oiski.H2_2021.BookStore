using Oiski.H2_2021.BookStore.DataLayer;
using Oiski.H2_2021.BookStore.DataLayer.Entities;
using Oiski.H2_2021.BookStore.DataLayer.Extensions;
using System;
using System.Text;

namespace Oiski.H2_2021.BookStore
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Assignment A
            //PrintBook(BookstoreRepo.Access.GetFirstBookIncluded());

            #region A.2
            //PrintAllBooks();
            #endregion
            #endregion

            //Book book = BookstoreRepo.Access.GetFirstBook();

            #region Assignment B
            //book.GetAuthers();
            //book.GetReviews();
            //PrintBook(book);

            #region B.2
            //Console.WriteLine($"Number of Reviews for this book: {book.GetReviewCount()}");
            //book.GetReviews();
            //foreach (var review in book.Reviews)
            //{
            //    Console.WriteLine($"Number of Stars: {review.NumStars}");
            //}
            #endregion
            #endregion

            #region Assignment C
            //Book book = BookstoreRepo.Access.GetFirstBook();
            //Console.WriteLine($"Title: {book.Title} - Price: {book.Price:00.00} - Number of Reviews: {book.GetReviewCount()}");
            #endregion

            //BookstoreRepo.Access.AddBookWithNewReview();
            BookstoreRepo.Access.AddBookWithOldAuthor();
        }

        private static void PrintBook(Book _book)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"BookID: {_book.BookID} - Title: {_book.Title} - Price: {_book.Price:00.00}");

            if (_book.BookAuthors != null)
            {
                foreach (var bookAuther in _book.BookAuthors)
                {
                    builder.AppendLine($"\tAuthor: {bookAuther.Author.Name}");
                }
            }

            if (_book.Reviews != null)
            {
                foreach (var rewiew in _book.Reviews)
                {
                    builder.AppendLine($"\tStars: {rewiew.NumStars} - Comment: {rewiew.Comment}");
                }
            }

            Console.WriteLine(builder.ToString());
            Console.ResetColor();
        }

        private static void PrintAllBooks()
        {
            foreach (var book in BookstoreRepo.Access.GetBooksAuthersIncluded())
            {
                PrintBook(book);
            }
        }
    }
}

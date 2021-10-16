using Microsoft.EntityFrameworkCore;
using Oiski.H2_2021.BookStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Oiski.H2_2021.BookStore.DataLayer.Extensions
{
    public static class BookExtensions
    {
        /// <summary>
        /// Explicit Loading as related data is loaded when needed (<i>We are performing a query on an object we already pulled</i>)
        /// </summary>
        /// <param name="_book"></param>
        public static void GetReviews(this Book _book)
        {
            using (var context = new BookstoreContext())
            {
                #region Legacy
                //_book.Reviews = context.Books
                //    .SelectMany(b => b.Reviews)
                //    .ToList();
                #endregion


                context.Attach(_book);

                //toReturn = context.Books
                //    .Single(b => b.BookID == _book.BookID);

                context.Entry(_book)
                    .Collection(b => b.Reviews)
                    .Load();
            }
        }

        /// <summary>
        /// Explicit Loading as related data is loaded when needed (<i>We are performing a query on an object we already pulled</i>)
        /// </summary>
        /// <param name="_book"></param>
        public static void GetAuthers(this Book _book)
        {
            using (var context = new BookstoreContext())
            {
                #region Legacy
                //_book.BookAuthors = context.Books
                //    .SelectMany(b => b.BookAuthors)
                //    .Where(ba => ba.BookID == _book.BookID)
                //    .Include(ba => ba.Author)
                //    .ToList();
                #endregion

                context.Attach(_book);

                context.Entry(_book)
                    .Collection(b => b.BookAuthors)
                    .Query()
                    .Include(ba => ba.Author)
                    .Load();
            }
        }

        /// <summary>
        /// Explicit Loading as related data is loaded when needed (<i>We are performing a query on an object we already pulled</i>)
        /// </summary>
        /// <param name="_book"></param>
        public static int GetReviewCount(this Book _book)
        {
            using (var context = new BookstoreContext())
            {
                context.Attach(_book);

                return context.Entry(_book)
                    .Collection(b => b.Reviews)
                    .Query()
                    .Count();
            }
        }
    }
}

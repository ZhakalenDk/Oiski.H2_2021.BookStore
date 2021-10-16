using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Oiski.H2_2021.BookStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Oiski.H2_2021.BookStore.DataLayer
{
    public class BookstoreContext : DbContext
    {
        private readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = BookstoreDB; Trusted_Connection = True; ";

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<PriceOffer> PriceOffers { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder _optionsBuilder)
        {
            Logger(_optionsBuilder);
        }

        private void Logger(DbContextOptionsBuilder _optionBuilder)
        {
            #region Legacy
            //_optionsBuilder.UseSqlServer(connectionString)
            //    .EnableSensitiveDataLogging(true)
            //    .UseLoggerFactory(new ServiceCollection()
            //    .AddLogging(builder => builder.AddConsole()
            //    .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information))
            //    .BuildServiceProvider().GetService<ILoggerFactory>());
            #endregion

            _optionBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging(true)
                .UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder _modelBuilder)
        {
            #region Joint Key
            _modelBuilder.Entity<BookAuthor>().HasKey(bookAuther => new { bookAuther.AuthorID, bookAuther.BookID });
            #endregion

            #region Relation Catalogue
            //_modelBuilder.Entity<Review>().HasOne(r => r.Book)
            //    .WithMany(b => b.Reviews)
            //    .HasForeignKey(r => r.BookID);

            //_modelBuilder.Entity<Book>()
            //    .HasOne(b => b.PriceOffer)
            //    .WithOne(p => p.Book)
            //    .HasForeignKey<PriceOffer>(p => p.BookID);
            #endregion

            GenerateSampleData(_modelBuilder);
        }

        private void GenerateSampleData(ModelBuilder _modelBuilder)
        {
            #region Books
            _modelBuilder.Entity<Book>().HasData(new Book { BookID = 1, Title = "Refactoring", Description = "Improving the design of the exsisting code", PublishedOn = DateTime.Parse("08.07.1999", new CultureInfo("da-DK"), System.Globalization.DateTimeStyles.AssumeLocal), Price = 40 });
            _modelBuilder.Entity<Book>().HasData(new Book { BookID = 2, Title = "Patterns of Enterprise Application Architecture", Description = "Written in direct response to the stuff challenges", PublishedOn = DateTime.Parse("15.11.2002", new CultureInfo("da-DK"), System.Globalization.DateTimeStyles.AssumeLocal), Price = 53 });
            _modelBuilder.Entity<Book>().HasData(new Book { BookID = 3, Title = "Domain-Driven Design", Description = "Linking business needs to software design", PublishedOn = DateTime.Parse("30.08.2003", new CultureInfo("da-DK"), System.Globalization.DateTimeStyles.AssumeLocal), Price = 56 });
            _modelBuilder.Entity<Book>().HasData(new Book { BookID = 4, Title = "Quantum Networking", Description = "Entangled quantum netorking provides faster-than-light data communications", PublishedOn = DateTime.Parse("01.01.2057", new CultureInfo("da-DK"), System.Globalization.DateTimeStyles.AssumeLocal), Price = 220 });
            #endregion

            #region Authors
            _modelBuilder.Entity<Author>().HasData(new Author { AuthorID = 1, Name = "Marting Fowler" });
            _modelBuilder.Entity<Author>().HasData(new Author { AuthorID = 2, Name = "Eric Evans" });
            _modelBuilder.Entity<Author>().HasData(new Author { AuthorID = 3, Name = "Future Person" });
            #endregion

            #region BookAuthor Catalogue
            _modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor { BookID = 1, AuthorID = 1 });
            _modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor { BookID = 1, AuthorID = 2 });
            _modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor { BookID = 2, AuthorID = 1 });
            _modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor { BookID = 3, AuthorID = 2 });
            _modelBuilder.Entity<BookAuthor>().HasData(new BookAuthor { BookID = 4, AuthorID = 3 });
            #endregion

            #region Reviews
            _modelBuilder.Entity<Review>().HasData(new Review { ReviewID = 1, BookID = 1, Comment = "Great Book", NumStars = 3 });
            _modelBuilder.Entity<Review>().HasData(new Review { ReviewID = 2, BookID = 1, Comment = "Boring Book", NumStars = 1 });
            #endregion
        }
    }
}

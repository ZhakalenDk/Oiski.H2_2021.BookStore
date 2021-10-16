using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Oiski.H2_2021.BookStore.DataLayer.Entities;

namespace Oiski.School.H3_2021.Bookstore.WebApp.Pages
{
    public class BooksDisplayModel : PageModel
    {
        private readonly Oiski.H2_2021.BookStore.DataLayer.BookstoreContext _context;

        public BooksDisplayModel(Oiski.H2_2021.BookStore.DataLayer.BookstoreContext context)
        {
            _context = context;
        }

        public List<Book> Book { get; set; }

        public async Task OnGet()
        {
            Book = await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.Reviews)
                .ToListAsync();
        }
    }
}

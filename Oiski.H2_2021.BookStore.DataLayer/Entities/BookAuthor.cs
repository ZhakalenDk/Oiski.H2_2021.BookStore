using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oiski.H2_2021.BookStore.DataLayer.Entities
{
    public class BookAuthor
    {
        public int BookID { get; set; } //  (Joint: Book|Auther) PK
        public Book Book { get; set; }  //  Reference Navigational Property
        public int AuthorID { get; set; } //  (Joint: Book|Auther) PK
        public Author Author { get; set; }  //  Reference Navigational Property
        public byte Order { get; set; }
    }
}

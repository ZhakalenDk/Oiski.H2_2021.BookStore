using System.Collections.Generic;

namespace Oiski.H2_2021.BookStore.DataLayer.Entities
{
    public class Author
    {
        public int AuthorID { get; set; }   //  PK
        public string Name { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }    //  (Joint: Book|Author) Collection Navigational Property
    }
}

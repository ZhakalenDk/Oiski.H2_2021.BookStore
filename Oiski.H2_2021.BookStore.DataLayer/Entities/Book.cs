using System;
using System.Collections.Generic;

namespace Oiski.H2_2021.BookStore.DataLayer.Entities
{
    public class Book
    {
        public int BookID { get; set; } //  PK
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool SoftDeleted { get; set; }

        public int PriceOfferID { get; set; }   //  FK
        public PriceOffer PriceOffer { get; set; }  //  Reference Navigational Property
        public ICollection<Review> Reviews { get; set; } = new List<Review>();   // Collection Navigational Property

        public ICollection<BookAuthor> BookAuthors { get; set; }    //  (Joint: Book|Author) Collection Navigational Property
    }
}

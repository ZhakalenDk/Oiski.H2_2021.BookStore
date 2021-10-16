using System.ComponentModel.DataAnnotations.Schema;

namespace Oiski.H2_2021.BookStore.DataLayer.Entities
{
    public class PriceOffer
    {
        public int PriceOfferID { get; set; }   //  PK
        public decimal NewPrice { get; set; }
        public string PromotionalText { get; set; }

        public int BookID { get; set; } //  FK
        [ForeignKey(nameof(BookID))]
        public Book Book { get; set; }  //  Reference Navigational Property
    }
}

namespace Oiski.H2_2021.BookStore.DataLayer.Entities
{
    public class Review
    {
        public int ReviewID { get; set; }   //  PK
        public string VoterName { get; set; }
        public int NumStars { get; set; }
        public string Comment { get; set; }
        
        public int BookID { get; set; } //  FK
        public Book Book { get; set; }  //  Reference Navigational Property
    }
}

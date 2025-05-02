using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLibrary
{
    
    public class Book
    {
        public string Titel { get; set; }
        public string Athor { get ; set ; }
        public int ISBN { get; set; }
        public string Caregory { get; set; }
        public bool Available { get; set; }
       

        public Book(string titel, string athor, int isbn, string caregory, bool available =true) {
            Titel = titel;
            Athor = athor;
            Caregory = caregory;
            ISBN = isbn;
            Available= available;
        }
        
        public BookRecord ToRecord()
        {
            return new BookRecord(Titel, Athor, ISBN, Caregory, Available);  //new Book(Titel, Athor,ISBN, Caregory);
        }
       
    }
}

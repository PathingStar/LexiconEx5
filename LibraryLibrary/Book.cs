using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
       

        public Book(string t, string a, int i, string c) {
            Titel = t;
            Athor = a;
            Caregory = c;
            ISBN = i;
            Available=true;
        }
        public BookRecord ToRecord()
        {
            return new BookRecord(Titel, Athor, ISBN, Caregory, Available);  //new Book(Titel, Athor,ISBN, Caregory);
        }
       
    }
}

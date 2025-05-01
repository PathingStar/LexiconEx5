using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLibrary
{
    internal class Book
    {
        string titel;
        string athor;
        int isbn;
        string caregory;
        bool available;

        public Book(string t, string a, int i, string c) {
            Titel = t;
            Athor = a;
            Caregory = c;
            ISBN = i;
            Available=true;
        }
        public BookRecord ToRecord()
        {
            return new BookRecord(Titel, athor, isbn, caregory, available);  //new Book(Titel, Athor,ISBN, Caregory);
        }
        public string Titel { get => titel; set => titel = value; }
        public string Athor { get => athor; set => athor = value; }
        public int ISBN { get => isbn; set => isbn = value; }
        public string Caregory { get => caregory; set => caregory = value; }
        public bool Available { get => available; set => available = value; }
    }
}

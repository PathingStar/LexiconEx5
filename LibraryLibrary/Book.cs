using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLibrary
{
    [DataContract]
    public class Book
    {
        [DataMember]
        string titel;
        [DataMember]
        string athor;
        [DataMember]
        int isbn;
        [DataMember]
        string caregory;
        [DataMember]
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

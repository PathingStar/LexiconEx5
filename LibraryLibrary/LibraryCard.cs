using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLibrary
{
    public class LibraryCard
    {
        //int Id { get; set; }
        public string Name { get; set; }

        public string Password {  get; set; }
        public List<Book> LoandBooks { get; set; }
        public LibraryCard(/*int id,*/ string name, string password) {
            //Id = id;
            Name = name;
            Password = password;
            LoandBooks = new List<Book>();
        }
        public bool AddBook(Book book) {
            if (!LoandBooks.Contains(book))
            {
                LoandBooks.Add(book);
                return true;
            }
            else return false;
        
        }
        public bool RemoveBook(Book book)
        {
            if (LoandBooks.Contains(book))
            {
                LoandBooks.Remove(book);
                return true;
            }
            else return false;

        }
    }
}

namespace LibraryLibrary
{
    public class Library
    {
        List<Book> books;
        private List<Book> Books {  get => books;  set => books = value; }
        private List<LibraryCard> LibraryCards { get; set; }

        private LibraryCard? LogedInCard { get; set; }
        //private List<int> 
        public int Count { get=>books.Count;  }
        public Library() {
            Books = new List<Book>();
            LibraryCards = new List<LibraryCard>();
            LogedInCard = null;
        }
        #region Book handling
        public bool RemoveBookByTitel(string titel)
        {
            try { return Books.Remove(Books.Find(b => b.Titel == titel)); }
            catch { return false; }
        }public bool RemoveBookByISBN(int isbn)
        {
            try { return Books.Remove(Books.Find(b => b.ISBN == isbn)); }
            catch { return false; }
        }
        public bool AddBook(BookRecord book)
        {
            if (!books.Exists(b => b.ISBN == book.ISBN)){
                books.Add(new Book(book.Titel, book.Athor,book.ISBN, book.Category));
                return true;
            }else return false;
        }
        public bool AddBook(string titel,
        string athor,
        int isbn,
        string caregory)
        {
            return AddBook(new BookRecord(titel, athor, isbn, caregory,true));
        }
        public List<BookRecord> GetBooks() {
            
            return books.Select(b => b.ToRecord()).ToList();
        
        }
        public List<BookRecord> GetBooksOrdedByTitel()
        {
            return GetBooks().OrderBy(b => b.Titel).ToList();
        }
        public List<BookRecord> GetBooksOrdedByAther()
        {
            return GetBooks().OrderBy(b => b.Athor).ToList();
        }
        public List<BookRecord> GetBooksByTitle(string title)
        {
            return books.Where(b => b.Titel.Contains(title)).Select(b=>b.ToRecord()).ToList();
        }public List<BookRecord> GetBooksByAther(string ather)
        {
            return books.Where(b => b.Athor == ather).Select(b=>b.ToRecord()).ToList();

        }
        public List<BookRecord> GetBooksByCategory(string category) {
        return books.Where(b => b.Caregory == category).Select(b => b.ToRecord()).ToList();
        }
        public BookRecord? GetBook(int isbn ){
            return books.Find(b => b.ISBN == isbn)?.ToRecord();
        }
        private Book? GetBookInternal(int isbn ){
            return books.Find(b => b.ISBN == isbn);
        }
        public bool Conteins(BookRecord book)
        {
            return books.Any(b=>b.ISBN == book.ISBN 
                            && b.Titel==book.Titel
                            && b.Athor==book.Athor
                            &&b.Caregory==book.Category);
        }
        #endregion

        #region Card handling
        public bool CheakIfCardExist(string name)
        {
            return LibraryCards.Any(c=>c.Name == name);
        }
        public bool MakeCard(string name,string password) {
            if (!CheakIfCardExist(name)) {
                LogedInCard = new LibraryCard(name, password);
                LibraryCards.Add(LogedInCard);
                return true;
            }else return false;
        }
        public bool LoginOnCard(string name, string password) {
            LibraryCard? card=LibraryCards.Find(b=>b.Name == name&&b.Password==password);
            if (card==null) return false;
            else {LogedInCard=card; return true;}
        }
        public string? WhoIsLogedin() {
            return LogedInCard?.Name;
        }
        public void Logout()
        {
            LogedInCard = null;
        }
        public bool BorrowBook(int isbn)
        {
            if (LogedInCard != null)
            {
                try {
                    Book book = GetBookInternal( isbn);
                    if (!LibraryCards.Any(c => c.LoandBooks.Contains(book))) {
                        book.Available=false;
                        LogedInCard.AddBook(book);
                        return true;
                    } else return false;
                
                
                }catch {return false;}

            }else return false;
        }
        public List<BookRecord>? GetMyBorrowedBooks()
        {
            return LogedInCard?.LoandBooks.Select(b=>b.ToRecord()).ToList();    
        }
        public bool ReturnBook(int isbn)
        {
            try {
               bool r= LogedInCard.RemoveBook(GetBookInternal(isbn));
                if(r) GetBookInternal(isbn).Available = true;
                return r;  
                    }catch {return false;}
        }
        public bool RemoveMyCard()
        {
            if(LogedInCard != null)
            {
                LibraryCards.Remove(LogedInCard);
                LogedInCard=null;
                return true;
            }else return false;
        }
        

        #endregion

    }
}

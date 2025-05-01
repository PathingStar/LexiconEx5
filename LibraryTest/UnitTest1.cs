using LibraryLibrary;

namespace LibraryTest
{
    public class UnitTest1
    {

        public class LibraryTests
        {
            private Library CreateLibraryWithBooks()
            {
                var library = new Library();
                library.AddBook("Test Book", "Author A", 12345, "Fiction");
                library.AddBook("Another Book", "Author B", 67890, "History");
                return library;
            }

            [Fact]
            public void AddBook_ShouldAddNewBook()
            {
                var library = new Library();
                var result = library.AddBook("Test Book", "Author A", 12345, "Fiction");
                Assert.True(result);
                Assert.Equal(1, library.Count);
            }

            [Fact]
            public void AddBook_ShouldNotAddDuplicateISBN()
            {
                var library = new Library();
                library.AddBook("Test Book", "Author A", 12345, "Fiction");
                var result = library.AddBook("Test Book 2", "Author B", 12345, "Drama");
                Assert.False(result);
                Assert.Equal(1, library.Count);
            }

            [Fact]
            public void RemoveBookByTitel_ShouldRemoveBook()
            {
                var library = CreateLibraryWithBooks();
                var result = library.RemoveBookByTitel("Test Book");
                Assert.True(result);
                Assert.Equal(1, library.Count);
            }

            [Fact]
            public void GetBooksByAther_ShouldReturnCorrectBooks()
            {
                var library = CreateLibraryWithBooks();
                var results = library.GetBooksByAther("Author B");
                Assert.Single(results);
                Assert.Equal("Another Book", results[0].Titel);
            }

            [Fact]
            public void MakeCard_ShouldCreateNewCard()
            {
                var library = new Library();
                var result = library.MakeCard("Alice", "pass123");
                Assert.True(result);
            }

            [Fact]
            public void LoginOnCard_ShouldSucceedWithCorrectCredentials()
            {
                var library = new Library();
                library.MakeCard("Alice", "pass123");
                var result = library.LoginOnCard("Alice", "pass123");
                Assert.True(result);
                Assert.Equal("Alice", library.WhoIsLogedin());
            }

            [Fact]
            public void BorrowBook_ShouldWorkWhenLoggedIn()
            {
                var library = CreateLibraryWithBooks();
                library.MakeCard("Alice", "pass123");
                library.LoginOnCard("Alice", "pass123");
                var result = library.BorrowBook(12345);
                Assert.True(result);
                var borrowed = library.GetMyBorrowedBooks();
                Assert.Single(borrowed);
                Assert.Equal(12345, borrowed[0].ISBN);
            }

            [Fact]
            public void ReturnBook_ShouldReturnSuccessfully()
            {
                var library = CreateLibraryWithBooks();
                library.MakeCard("Alice", "pass123");
                library.LoginOnCard("Alice", "pass123");
                library.BorrowBook(12345);
                var result = library.ReturnBook(12345);
                Assert.True(result);
                Assert.Empty(library.GetMyBorrowedBooks());
            }
        }


    }
}
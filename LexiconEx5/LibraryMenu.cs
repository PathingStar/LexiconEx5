using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using LibraryLibrary;

namespace LexiconEx5
{
    public static class LibraryMenu
    {
        public static void Start()
        {
            
            Library lib;
            try { lib = JsonSerializer.Deserialize<Library>(File.ReadAllText("Library.json"), new JsonSerializerOptions {  IncludeFields = true });
                lib.Logout();
                Console.WriteLine("secsesful loded Libery");
            }
            catch(Exception e) { lib = new Library();
                Console.WriteLine(e.Message);
            }
            char input = ' '; //Creates the character input to be used with the switch-case below.
            string inputS;
            while (true)
            {

                Console.WriteLine($"Welcom{((lib.WhoIsLogedin() != null) ? " " + lib.WhoIsLogedin() : "")} Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                    + $"\n1. {((lib.WhoIsLogedin() != null) ? "Logout" : "Login")}"
                    + "\n2. Make a libary card"
                    + "\n3. Add new book"
                    + "\n4. Lock for books"
                    //+ "\n5. Fibonacci sequence"
                    //+ "\n6. Even number"
                    + "\n0. Exit the application"
                    );

                try
                {
                    input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                }
                catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                {
                    Console.Clear();
                    Console.WriteLine("Please enter some input!");
                }
                switch (input)
                {
                    case '1':

                        if (lib.WhoIsLogedin() != null) lib.Logout(); else LoginNameCheak();

                        break;
                    case '2':
                        MakeCard();
                        break;

                    case '3':
                        AddBook();
                        break;
                    case '4':
                        LockForBooks();
                        break;
                    case '0':
                        Environment.Exit(0);
                        break;

                    default:
                        break;

                }
            }
            void Save()
            {
                File.WriteAllText("Library.json",lib.Serialize());
            }
            void LoginNameCheak()
            {
                while (true)
                {
                    Console.Write("User Name: ");
                    inputS = Console.ReadLine();
                    if (lib.CheakIfCardExist(inputS))
                    {
                        LoginPasswordCheak(inputS);
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"No user {inputS}. input '0' to go back to main menu. input eniting else to try agen");
                        if (Console.ReadLine()![0] == '0') break;
                    }

                }
            }
            void LoginPasswordCheak(string name)
            {
                while (true)
                {
                    Console.Write("User Name: ");
                    inputS = Console.ReadLine();
                    if (lib.LoginOnCard(name, inputS))
                    {
                        Console.WriteLine($"welcom {name}");
                        break;
                    }
                    else Console.WriteLine("Wrong password try agen");
                }
            }
            void MakeCard()
            {
                while (true)
                {
                    Console.Write("User name: ");
                    inputS = Console.ReadLine();
                    Console.Write("Password: ");
                    if (lib.MakeCard(inputS, Console.ReadLine()))
                    {
                        Console.WriteLine($"welcom {inputS}");
                        Save();
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"the name {inputS} is in use. input '0' to go back to main menu. input eniting else to try agen");
                        if (Console.ReadLine()![0] == '0') break;
                    }

                }
            }
            void AddBook()
            {
                string name, ather, category;
                int isbn;
                while (true)
                {
                    try
                    {
                        Console.Write("Book name: ");
                        name = Console.ReadLine();
                        Console.Write("Ather name: ");
                        ather = Console.ReadLine();
                        Console.Write("Category: ");
                        category = Console.ReadLine();
                        Console.Write("ISBN number: ");


                        isbn = int.Parse(Console.ReadLine());
                        if (lib.AddBook(new BookRecord(name, ather, isbn, category, true)))
                        {
                            Save();
                            break;
                        }
                        else throw new Exception();

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("wrong input or ISBN as alredy in use input '0' to go back to main menu. input eniting else to try agen");
                        try { if (Console.ReadLine()![0] == '0') break; } catch { }
                    }

                }

            }
            void LockForBooks()
            {
                List<BookRecord> books;
                bool run = true;
                while (run)
                {
                    try
                    {
                        Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                   + "\n1. Get all books"
                   + "\n2. Search for titel"
                   + "\n3. Search for ather"
                   + "\n4. Search for category"
                   + "\n5. Get book by ISBN number"
                   + "\n0. Go back");
                        try
                        {
                            input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                        }
                        catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                        {

                            Console.WriteLine("Please enter some input!");
                            continue;
                        }
                        switch (input)
                        {
                            case '1':
                                ShowBooks(() => lib.GetBooks()); break;
                            case '2':
                                Console.Write("Contains in titel: ");
                                ShowBooks(() => lib.GetBooksByTitle(Console.ReadLine()));
                                break;
                            case '3':
                                Console.Write("Atherd by: ");
                                ShowBooks(() => lib.GetBooksByAther(Console.ReadLine()));
                                break;
                            case '4':
                                Console.Write("In Category:");
                                ShowBooks(()=>lib.GetBooksByCategory(Console.ReadLine()));
                                break;
                            case '5':
                                Console.Write("ISBN: ");
                                ShowBooks(() => new List<BookRecord>() { lib.GetBook(int.Parse(Console.ReadLine())) });
                                break;
                            case '0':
                                run = false; break;
                            default: break;

                        }

                    }
                    catch
                    {
                        Console.WriteLine("Incorect input");
                    }


                }
            }
            void ShowBooks(Func< List<BookRecord>> bookOrder)
            {
                bool run = true;
                List<BookRecord> books;
                while (run)
                {
                    Console.WriteLine();
                    books = bookOrder();
                    foreach (BookRecord book in books) Console.WriteLine(book.ToString());

                    Console.WriteLine("Please navigate through the menu by inputting the number \n(1, 2, 3 ,4, 0) of your choice"
                    + "\n1. order by titel"
                    + "\n2. order by ather"
                    + "\n3. order by category"
                    + "\n4. borrow book"
                    + "\n5. remove book"
                    + "\n0. Go back");
                    try
                    {
                        input = Console.ReadLine()![0]; //Tries to set input to the first char in an input line
                    }
                    catch (IndexOutOfRangeException) //If the input line is empty, we ask the users for some input.
                    {

                        Console.WriteLine("Please enter some input!");
                        continue;
                    }
                    switch (input)
                    {
                        case '1':
                            books.OrderBy(b => b.Titel);
                            break;
                        case '2':
                            books.OrderBy(b => b.Athor);

                            break;
                        case '3':
                            books.OrderBy(b => b.Category);
                            break;
                        case '4':
                            if (lib.WhoIsLogedin() != null) BorrowBook();
                            run = false;
                            break;
                        case '5':
                            removeBook();
                            break;
                        case '0':
                            run = false;
                            break;
                        default: break;
                    }
                }

            }
            void BorrowBook()
            {
                while (true)
                {
                    try
                    {
                        Console.Write("ISBN of the book you want to borrow: ");
                        inputS = Console.ReadLine();
                        if (lib.BorrowBook(int.Parse(inputS)))
                        {
                            Console.WriteLine("you borrowed " + lib.GetBook(int.Parse(inputS)));
                            Save();
                            break;
                        }
                        else throw new Exception();
                    }
                    catch
                    {

                        Console.WriteLine("incorect ISBN, input '0' to go back, enyting to try agen");
                        if (Console.ReadLine() == "0") break;
                    }
                }
            }
            void removeBook()
            {
                string rBook;
                while (true)
                {
                    try
                    {
                        Console.Write("ISBN of the book you want to borrow: ");
                        inputS = Console.ReadLine();
                        rBook = lib.GetBook(int.Parse(inputS)).ToString();
                        if (lib.RemoveBookByISBN(int.Parse(inputS)))
                        {
                            Console.WriteLine("you have removed" + rBook);
                            Save();
                            break;
                        }
                        else throw new Exception();
                    }
                    catch
                    {
                        Console.WriteLine("incorect ISBN, input '0' to go back, enyting to try agen");
                        if (Console.ReadLine() == "0") break;
                    }
                }
            }
        }
    }
}

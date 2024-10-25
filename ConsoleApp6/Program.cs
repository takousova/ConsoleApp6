

public class Book
{
    private string title;
    private string author;
    private string isbn;
    private int    copiesAvailable;

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Author
    {
        get { return author; }
        set { author = value; }
    }

    public string ISBN
    {
        get { return isbn; }  // Getter added here
        set
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length != 13)
            {
                throw new ArgumentException("ISBN must be a 13-character string.");
            }
            isbn = value;
        }
    }

    public int CopiesAvailable
    {
        get { return copiesAvailable; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Copies available cannot be negative.");
            }
            copiesAvailable = value;
        }
    }

    public Book(string title, string author, string isbn, int copiesAvailable)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        CopiesAvailable = copiesAvailable;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Title: {Title}, Author: {Author}, ISBN: {ISBN}, Copies Available: {CopiesAvailable}");
    }

    public bool BorrowBook()
    {
        if (CopiesAvailable > 0)
        {
            CopiesAvailable--;
            Console.WriteLine($"You've borrowed \"{Title}\".");
            return true;
        }
        else
        {
            Console.WriteLine($"No copies of \"{Title}\" are available.");
            return false;
        }
    }

    public void ReturnBook()
    {
        CopiesAvailable++;
        Console.WriteLine($"You've returned \"{Title}\".");
    }
}

public class Library
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
        Console.WriteLine($"Book \"{book.Title}\" added to the library.");
    }

    public void RemoveBook(string isbn)
    {
        Book bookToRemove = books.Find(b => b.ISBN == isbn);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Book \"{bookToRemove.Title}\" removed from the library.");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void DisplayAllBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books in the library.");
            return;
        }

        foreach (var book in books)
        {
            book.DisplayInfo();
        }
    }

    public Book SearchBook(string title)
    {
        return books.Find(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }
}

partial class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        while (true)
        {
            Console.WriteLine("\nLibrary Menu:");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Display All Books");
            Console.WriteLine("4. Search Book");
            Console.WriteLine("5. Borrow Book");
            Console.WriteLine("6. Return Book");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter title: ");
                    string title = Console.ReadLine();
                    Console.Write("Enter author: ");
                    string author = Console.ReadLine();
                    Console.Write("Enter ISBN (13 characters): ");
                    string isbn = Console.ReadLine();
                    Console.Write("Enter number of copies available: ");
                    int copies = int.Parse(Console.ReadLine());

                    try
                    {
                        Book book = new Book(title, author, isbn, copies);
                        library.AddBook(book);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "2":
                    Console.Write("Enter ISBN of the book to remove: ");
                    string isbnToRemove = Console.ReadLine();
                    library.RemoveBook(isbnToRemove);
                    break;

                case "3":
                    library.DisplayAllBooks();
                    break;

                case "4":
                    Console.Write("Enter title of the book to search: ");
                    string searchTitle = Console.ReadLine();
                    Book foundBook = library.SearchBook(searchTitle);
                    if (foundBook != null)
                    {
                        foundBook.DisplayInfo();
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case "5":
                    Console.Write("Enter title of the book to borrow: ");
                    string borrowTitle = Console.ReadLine();
                    Book borrowBook = library.SearchBook(borrowTitle);
                    if (borrowBook != null)
                    {
                        borrowBook.BorrowBook();
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case "6":
                    Console.Write("Enter title of the book to return: ");
                    string returnTitle = Console.ReadLine();
                    Book returnBook = library.SearchBook(returnTitle);
                    if (returnBook != null)
                    {
                        returnBook.ReturnBook();
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break;

                case "7":
                    Console.WriteLine("Exiting the program.");
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
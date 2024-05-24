Library library = new Library();

library.AddBook();
Book book1 = new Book("Hobit", "Tolkien", "HBT101", 111);
library.UpdateBook(ref book1, "Hobit 2");

Book book2;
library.FindBook("Hobit", out book2);
Console.WriteLine(book2.ISBN);

Console.ReadKey();

class Book
{
    public string Title { get; set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public int NumberOfPages { get; private set; }

    public Book(string title, string author, string isbn, int numberOfPages)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        NumberOfPages = numberOfPages;
    }

    protected Book() { }

    internal void DisplayDetails()
    {
        Console.WriteLine($"Name: {this.Title} by {this.Author} has {this.NumberOfPages} pages| ISBN: {this.ISBN}");
    }
}

class Library
{
    List<Book> books = new List<Book> { };

    public void AddBook()
    {
        Console.WriteLine("Enter name: ");
        string name = Console.ReadLine();

        Console.WriteLine("Enter author: ");
        string author = Console.ReadLine();

        Console.WriteLine("Enter pages: ");
        int pages = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter ISBN: ");
        string isbn = Console.ReadLine();

        Book book = new Book(name, author, isbn, pages);

        books.Add(book);
        book.DisplayDetails();

    }

    public void FindBook(string title, out Book book)
    {
        book =  books.Find(b => b.Title == title);
    }

    public void UpdateBook(ref Book book, string newTitle)
    {
        book.Title = newTitle;
    }
}


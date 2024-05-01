using MyWebApp.Models;

public interface IBookService
{
    IEnumerable<Book> GetBooks();

    Book GetBook(int id);
}
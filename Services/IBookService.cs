using MyWebApp.Models;

namespace Learning.FastEndpoionts.Services;

/// <summary>
/// Interface for the book service.
/// </summary>
public interface IBookService
{
    /// <summary>
    /// Gets all books.
    /// </summary>
    /// <returns></returns>
    IEnumerable<Book> GetBooks();

    /// <summary>
    /// Gets a book by its ID.
    /// </summary>
    /// <param name="id">ID of the book to search for.</param>
    /// <returns>If it exists, will return the book, otherwise null.</returns>
    Book? GetBook(int id);
}
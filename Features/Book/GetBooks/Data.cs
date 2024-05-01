namespace Learning.FastEndpoionts.Features.Book.GetBooks;

public static class Data
{
    /// <summary>
    /// Gets all books from the data source.
    /// </summary>
    /// <returns>List of the Books currently available.</returns>
    internal static IList<Models.Book> GetAllBooks() => DataSource.Books;
}
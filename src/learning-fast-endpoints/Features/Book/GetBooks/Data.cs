namespace Learning.FastEndpoionts.Features.Book.GetBooks;

/// <summary>
/// Data access methods for the GetBooks feature.
/// </summary>
public static class Data
{
    /// <summary>
    /// Gets all books from the data source.
    /// </summary>
    /// <returns>List of the Books currently available.</returns>
    internal static IEnumerable<Models.Book> GetAllBooks() => DataSource.Books;
}
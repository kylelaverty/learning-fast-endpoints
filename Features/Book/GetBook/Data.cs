namespace Learning.FastEndpoionts.Features.Book.GetBook; 

/// <summary>
/// Data access methods for the GetBook feature.
/// </summary>
public static class Data
{
    /// <summary>
    /// Gets a book by its ID.
    /// </summary>
    /// <param name="id">The ID of the book to get.</param>
    /// <returns>The book with the given ID, or null if not found.</returns>
    internal static Models.Book? GetBookById(int id) => DataSource.Books.FirstOrDefault(x => x.Id == id);
}
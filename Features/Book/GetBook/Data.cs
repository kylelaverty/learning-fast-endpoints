namespace Learning.FastEndpoionts.Features.Book.GetBook; 

public static class Data
{
    /// <summary>
    /// Gets all books from the data source.
    /// </summary>
    /// <returns>List of the Books currently available.</returns>
    internal static Models.Book? GetBookById(int id) => DataSource.Books.FirstOrDefault(x => x.Id == id);
}
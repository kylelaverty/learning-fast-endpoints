namespace Learning.FastEndpoionts.Models; 

/// <summary>
/// A book model.
/// </summary>
public class Book
{
    /// <summary>
    /// The id of the book.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title of the book.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// The author of the book.
    /// </summary>
    public required string Author { get; set; }
}
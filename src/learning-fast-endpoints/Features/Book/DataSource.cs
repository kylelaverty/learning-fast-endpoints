namespace Learning.FastEndpoionts.Features.Book;

/// <summary>
/// Serve as a temp data source for the Book feature.
/// </summary>
public static class DataSource
{
    public static IList<Models.Book> Books { get; } =
    [
        new()
        {
            Id = 1,
            Title = "Dependency Injection in .NET",
            Author = "Mark Seemann"
        },
        new()
        {
            Id = 2,
            Title = "C# in Depth",
            Author = "Jon Skeet"
        },
        new()
        {
            Id = 3,
            Title = "Programming Entity Framework",
            Author = "Julia Lerman"
        },
        new()
        {
            Id = 4,
            Title = "Programming WCF Services",
            Author = "Juval Lowy and Michael Montgomery"
        }
    ];
}
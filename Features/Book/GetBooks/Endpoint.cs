using Microsoft.AspNetCore.Http.HttpResults;

namespace Features.Book.GetBooks;

public class Endpoint : EndpointWithoutRequest<Results<Ok<IEnumerable<MyWebApp.Models.Book>>, NotFound>>
{
    private readonly IBookService _bookService;

    public Endpoint(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<IEnumerable<MyWebApp.Models.Book>>, NotFound>> ExecuteAsync(CancellationToken ct)
    {
        var books = _bookService.GetBooks();

        if (books == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(books);
    }
}
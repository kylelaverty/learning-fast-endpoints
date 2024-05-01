using Microsoft.AspNetCore.Http.HttpResults;

namespace Features.Book.GetBook;

public class Endpoint : Endpoint<Request, Results<Ok<MyWebApp.Models.Book>, NotFound>>
{
    private readonly IBookService _bookService;

    public Endpoint(IBookService bookService)
    {
        _bookService = bookService;
    }

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<MyWebApp.Models.Book>, NotFound>> ExecuteAsync(Request request, CancellationToken ct)
    {
        var book = _bookService.GetBook(request.Id);
        if (book == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(book);
    }
}
using Learning.FastEndpoionts.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.Book.GetBook;

public class Endpoint(IBookService bookService) : Endpoint<Request, Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>> ExecuteAsync(Request request, CancellationToken ct)
    {
        var book = _bookService.GetBook(request.Id);

        return book == null ? TypedResults.NotFound() : TypedResults.Ok(book);
    }
}
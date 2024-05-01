using Learning.FastEndpoionts.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.Book.GetBooks;

public class Endpoint(IBookService bookService) : EndpointWithoutRequest<Results<Ok<IEnumerable<Learning.FastEndpoionts.Models.Book>>, NotFound>>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<IEnumerable<Learning.FastEndpoionts.Models.Book>>, NotFound>> ExecuteAsync(CancellationToken ct)
    {
        var books = _bookService.GetBooks();

        return books == null || !books.Any()
            ? TypedResults.NotFound() 
            : TypedResults.Ok(books);
    }
}
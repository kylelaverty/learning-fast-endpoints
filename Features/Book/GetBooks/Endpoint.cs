using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.Book.GetBooks;

public class Endpoint() : EndpointWithoutRequest<Results<Ok<IList<Learning.FastEndpoionts.Models.Book>>, NotFound>>
{
    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<IList<Learning.FastEndpoionts.Models.Book>>, NotFound>> ExecuteAsync(CancellationToken ct)
    {
        var books = Data.GetAllBooks();

        return books == null || !books.Any()
            ? TypedResults.NotFound() 
            : TypedResults.Ok(books);
    }
}
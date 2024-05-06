using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.Book.GetBooks;

/// <summary>
/// Endpoint for getting all the books.
/// </summary>
public class Endpoint() : EndpointWithoutRequest<Results<Ok<IList<Learning.FastEndpoionts.Models.Book>>, NotFound>>
{
    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
    }

    /// <summary>
    /// Executes the endpoint to get all the books.
    /// </summary>
    /// <param name="cancellationToken">A token that can be observed to cancel the operation.</param>
    /// <returns></returns>
    public override async Task<Results<Ok<IList<Learning.FastEndpoionts.Models.Book>>, NotFound>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var books = Data.GetAllBooks();

        return books == null || !books.Any()
            ? TypedResults.NotFound() 
            : TypedResults.Ok(books);
    }
}
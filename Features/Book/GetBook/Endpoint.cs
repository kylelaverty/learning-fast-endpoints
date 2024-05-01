using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.Book.GetBook;

/// <summary>
/// Endpoint for getting a book by its ID.
/// </summary>
/// <param name="logger"><see cref="ILogger"/> used for logging errors and messages.</param>
public class Endpoint(ILogger<Endpoint>? logger) : Endpoint<Request, Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>>
{
    /// <summary>
    /// The logger for the endpoint.
    /// </summary>
    private readonly ILogger<Endpoint>? _logger = logger;

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
    }

    /// <summary>
    /// Executes the endpoint to get a book by its ID.
    /// </summary>
    /// <param name="request"><see cref="Request"/> for searching for a book.</param>
    /// <param name="cancellationToken">A token that can be observed to cancel the operation.</param>
    /// <returns></returns>
    public override async Task<Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>> ExecuteAsync(Request request, CancellationToken cancellationToken = default)
    {
        _logger?.LogInformation("Getting book with ID {Id}", request.Id);

        var book = Data.GetBookById(request.Id);

        return book == null ? TypedResults.NotFound() : TypedResults.Ok(book);
    }
}
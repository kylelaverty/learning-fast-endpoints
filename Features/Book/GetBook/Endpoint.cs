using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.Book.GetBook;

public class Endpoint(ILogger<Endpoint>? logger) : Endpoint<Request, Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>>
{
    private readonly ILogger<Endpoint>? _logger = logger;

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>> ExecuteAsync(Request request, CancellationToken ct)
    {
        _logger?.LogInformation("Getting book with ID {Id}", request.Id);

        var book = Data.GetBookById(request.Id);

        return book == null ? TypedResults.NotFound() : TypedResults.Ok(book);
    }
}
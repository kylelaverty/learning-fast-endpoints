using Microsoft.AspNetCore.Http.HttpResults;
using Unleash;

namespace Learning.FastEndpoionts.Features.Book.GetBook;

/// <summary>
/// Endpoint for getting a book by its ID.
/// </summary>
public class Endpoint(IUnleash featureToggles) : Endpoint<Request, Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>>
{
    private readonly IUnleash _featureToggles = featureToggles;

    public override void Configure()
    {
        Get("/books/{id}");
        AllowAnonymous();
        Throttle(
            hitLimit: 10,
            durationSeconds: 30
        );
    }

    /// <summary>
    /// Executes the endpoint to get a book by its ID.
    /// </summary>
    /// <param name="request"><see cref="Request"/> for searching for a book.</param>
    /// <param name="cancellationToken">A token that can be observed to cancel the operation.</param>
    /// <returns></returns>
    public override async Task<Results<Ok<Learning.FastEndpoionts.Models.Book>, NotFound>> ExecuteAsync(Request request, CancellationToken cancellationToken = default)
    {
        Logger?.LogInformation("Getting book with ID {Id}", request.Id);

        if (_featureToggles.IsEnabled(Utils.FeatureToggles.FeatureToggleList.LFEBookTesting))
        {
            Logger?.LogInformation("Toggle is enabled.");
        }

        var book = Data.GetBookById(request.Id);

        return book == null ? TypedResults.NotFound() : TypedResults.Ok(book);
    }
}
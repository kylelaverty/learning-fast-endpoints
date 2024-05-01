using Microsoft.AspNetCore.Http.HttpResults;

namespace Learning.FastEndpoionts.Features.User.Create;

public class Endpoint : Endpoint<Request, 
                                   Results<Ok<Response>, 
                                           NotFound>>
{
    public override void Configure()
    {
        Post("/user/create");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<Response>, NotFound>> ExecuteAsync(Request req, CancellationToken ct)
    {
        return req.Age == -1
            ? TypedResults.NotFound()
            : TypedResults.Ok(new Response()
        {
            FullName = req.FirstName + " " + req.LastName,
            IsOver18 = req.Age > 18
        });
    }
}
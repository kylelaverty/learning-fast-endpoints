using Microsoft.AspNetCore.Http.HttpResults;

public class MyEndpoint : Endpoint<MyRequest, 
                                   Results<Ok<MyResponse>, 
                                           NotFound>>
{
    public override void Configure()
    {
        Post("/user/create");
        AllowAnonymous();
    }

    public override async Task<Results<Ok<MyResponse>, NotFound>> ExecuteAsync(MyRequest req, CancellationToken ct)
    {

        if (req.Age == -1)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new MyResponse()
        {
            FullName = req.FirstName + " " + req.LastName,
            IsOver18 = req.Age > 18
        });
    }
}
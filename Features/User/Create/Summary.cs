namespace Learning.FastEndpoionts.Features.User.Create; 

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "short summary goes here";
        Description = "long description goes here";
        Response<Response>(200, "ok response with body");
        Response<ErrorResponse>(400, "validation failure");
        Response(404, "account not found");
    }
}
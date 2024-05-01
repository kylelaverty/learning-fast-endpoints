public class MySummary : Summary<MyEndpoint>
{
    public MySummary()
    {
        Summary = "short summary goes here";
        Description = "long description goes here";
        Response<MyResponse>(200, "ok response with body");
        Response<ErrorResponse>(400, "validation failure");
        Response(404, "account not found");
    }
}
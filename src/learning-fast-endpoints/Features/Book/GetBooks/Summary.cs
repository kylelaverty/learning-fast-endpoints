namespace Learning.FastEndpoionts.Features.Book.GetBooks;

/// <summary>
/// The Swagger docs summary data for the GetBooks endpoint.
/// </summary>
public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Get all the books in the system";
        Description = "Return a listing of all of the books in the entire system with no filters or restrictions of any kind. This is a public endpoint and does not require authentication.";
        Response<IEnumerable<Learning.FastEndpoionts.Models.Book>>(200, "Books were found and are returned.");
        Response(404, "No books were found.");
    }
}
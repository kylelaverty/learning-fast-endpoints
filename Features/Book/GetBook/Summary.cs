namespace Learning.FastEndpoionts.Features.Book.GetBook;

public class Summary : Summary<Endpoint>
{
    public Summary()
    {
        Summary = "Get a book from the system for the given ID";
        Description = "Returns a single book from the system for the given ID. This is a public endpoint and does not require authentication.";
        Response<IEnumerable<Learning.FastEndpoionts.Models.Book>>(200, "Book was found and is returned.");
        Response(404, "Book was not found.");
    }
}
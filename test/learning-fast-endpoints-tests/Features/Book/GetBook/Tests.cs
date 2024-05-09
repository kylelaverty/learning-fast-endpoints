using Learning.FastEndpoionts.Features.Book.GetBook;

namespace Learning.FastEndpoionts.Tests.Features.Book.GetBook;

public class Tests(App app) : TestBase<App>
{
    [Fact]
    public async Task Invalid_Id_Request()
    {
        var client = app.CreateClient();
        var (response, result) = await client.GETAsync<Endpoint, Request, ErrorResponse>(new() { Id = 0 });
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        result.Should().NotBeNull();
        result.Errors.Count.Should().Be(1);
        result.Errors.Keys.Should().Equal("id");
    }

    [Fact]
    public async Task Missing_Book_Id_Request()
    {
        var client = app.CreateClient();
        var (response, result) = await client.GETAsync<Endpoint, Request, ErrorResponse>(new() { Id = 99999 });
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        result.Should().BeNull();
    }

    [Fact]
    public async Task Working_Id_Request()
    {
        var targetBook = FastEndpoionts.Features.Book.DataSource.Books[0];
        var client = app.CreateClient();
        var (response, result) = await client.GETAsync<Endpoint, Request, Models.Book>(new() { Id = targetBook.Id });
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Id.Should().Be(targetBook.Id);
        result.Author.Should().Be(targetBook.Author);
        result.Title.Should().Be(targetBook.Title);
    }
}
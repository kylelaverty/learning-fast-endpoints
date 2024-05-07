﻿using Learning.FastEndpoionts.Features.Book.GetBooks;

namespace Learning.FastEndpoionts.Tests.Features.Book.GetBooks;

public class Tests(App app) : TestBase<App>
{
    [Fact]
    public async Task Get_Books_Result()
    {
        var targetBook = FastEndpoionts.Features.Book.DataSource.Books;

        var (response, result) = await app.Client.GETAsync<Endpoint, IEnumerable<Models.Book>>();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        result.Should().NotBeNull();
        result.Count().Should().Be(targetBook.Count());
    }   
}
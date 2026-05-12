using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Frontend.Tests;

public class UserReaderServiceHttpTests
{
    private class FakeHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(_response);
        }
    }

    private HttpClient CreateClient(HttpResponseMessage response)
    {
        return new HttpClient(new FakeHandler(response))
        {
            BaseAddress = new Uri("http://localhost/")
        };
    }

    [Fact]
    public async Task GetAvailableBooks_ReturnsSuccessJson()
    {
        // Arrange
        var data = new[]
        {
            new { Title = "Book1", Author = "A1" }
        };

        var json = JsonSerializer.Serialize(data);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var client = CreateClient(response);

        // Act
        var result = await client.GetStringAsync("api/Reader/GetAvailableBooks");

        // Assert
        Assert.Contains("Book1", result);
    }

    [Fact]
    public async Task GetMyLoans_ReturnsSuccessJson()
    {
        // Arrange
        var data = new[]
        {
            new { ReaderName = "John", BookTitle = "Book1" }
        };

        var json = JsonSerializer.Serialize(data);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        var client = CreateClient(response);

        // Act
        var result = await client.GetStringAsync("api/Reader/GetLoansBy/1");

        // Assert
        Assert.Contains("John", result);
    }
}
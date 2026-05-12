using Konyvtari_nyilvantarto.Controllers;
using Konyvtari_nyilvantarto.Repositories;
using NSubstitute;
using Share.Dtos;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Konyvtari_nyilvantarto.Tests;

public class ReaderControllerTests
{
    [Fact]
    public async Task GivenNoReader_WhenGetLoansCalled_ThenReturnsNotFound()
    {
        // Arrange
        var repo = Substitute.For<IReaderRepository>();

        repo.GetLoansByReaderIdAsync(1).Returns((IEnumerable<ReaderLoanListDto>?)null);

        var controller = new ReaderController(repo);

        // Act
        var result = await controller.GetLoansByReaderIdAsync(1);

        // Assert
        var notFound = Assert.IsType<NotFoundObjectResult>(result.Result);
        Assert.Contains("1", notFound.Value!.ToString());
    }

    [Fact]
    public async Task GivenReaderExists_WhenGetLoansCalled_ThenReturnsOk()
    {
        // Arrange
        var repo = Substitute.For<IReaderRepository>();

        repo.GetLoansByReaderIdAsync(1).Returns(new List<ReaderLoanListDto>
        {
            new ReaderLoanListDto
            {
                ReaderName = "Test Reader",
                BookTitle = "Book 1"
            }
        });

        var controller = new ReaderController(repo);

        // Act
        var result = await controller.GetLoansByReaderIdAsync(1);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var data = Assert.IsType<List<ReaderLoanListDto>>(ok.Value);

        Assert.Single(data);
    }

    [Fact]
    public async Task GivenBooksExist_WhenGetAvailableBooksCalled_ThenReturnsOk()
    {
        // Arrange
        var repo = Substitute.For<IReaderRepository>();

        repo.GetAvailableBooksAsync().Returns(new List<ReaderBookListDto>
        {
            new ReaderBookListDto
            {
                Title = "Book 1",
                Author = "Author 1"
            }
        });

        var controller = new ReaderController(repo);

        // Act
        var result = await controller.GetAvailableBooksAsync();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var data = Assert.IsType<List<ReaderBookListDto>>(ok.Value);

        Assert.Single(data);
    }
}
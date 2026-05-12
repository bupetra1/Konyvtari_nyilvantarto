using Konyvtari_nyilvantarto.Repositories;
using NSubstitute;
using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class IReaderRepositoryTests
{
    [Fact]
    public async Task GivenAvailableBooks_WhenGetAvailableBooksCalled_ThenReturnsBookList()
    {
        // Arrange
        var repositoryMock = Substitute.For<IReaderRepository>();

        var books = new List<ReaderBookListDto>
        {
            new ReaderBookListDto()
        };

        repositoryMock
            .GetAvailableBooksAsync()
            .Returns(books);

        // Act
        var result = await repositoryMock.GetAvailableBooksAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async Task GivenReaderId_WhenGetLoansCalled_ThenReturnsLoanList()
    {
        // Arrange
        var repositoryMock = Substitute.For<IReaderRepository>();

        var loans = new List<ReaderLoanListDto>
        {
            new ReaderLoanListDto()
        };

        repositoryMock
            .GetLoansByReaderIdAsync(1)
            .Returns(loans);

        // Act
        var result = await repositoryMock.GetLoansByReaderIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
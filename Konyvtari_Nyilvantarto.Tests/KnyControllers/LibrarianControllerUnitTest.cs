using Konyvtari_nyilvantarto.Controllers;
using Konyvtari_nyilvantarto.Repositories;
using NSubstitute;
using Share.Dtos;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace Konyvtari_nyilvantarto.Tests;

public class LibrarianControllerTests
{
    [Fact]
    public async Task GivenBooksExist_WhenGetBooksCalled_ThenReturnsOkWithList()
    {
        // Arrange
        var repo = Substitute.For<ILibrarianRepository>();

        repo.GetBooksAsync().Returns(new List<BookDto>
        {
            new BookDto { BookId = 1, Title = "Book1", Author = "Author1" }
        });

        var controller = new LibrarianController(repo);

        // Act
        var result = await controller.GetBooksAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var data = Assert.IsType<List<BookDto>>(okResult.Value);

        Assert.Single(data);
    }

    [Fact]
    public async Task GivenValidBook_WhenCreateBookCalled_ThenReturnsOk()
    {
        // Arrange
        var repo = Substitute.For<ILibrarianRepository>();

        var dto = new CreateBookDto
        {
            Title = "Test",
            Author = "Author"
        };

        repo.CreateBookAsync(dto).Returns(new Book());

        var controller = new LibrarianController(repo);

        // Act
        var result = await controller.CreateBookAsync(dto);

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(dto, ok.Value);
    }

    [Fact]
    public async Task GivenBookNotAvailable_WhenCreateLoanCalled_ThenReturnsConflict()
    {
        // Arrange
        var repo = Substitute.For<ILibrarianRepository>();

        repo.IsBookAvailableAsync(1).Returns(false);

        var controller = new LibrarianController(repo);

        var dto = new CreateLoanDto
        {
            BookId = 1,
            ReaderId = 1,
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(5))
        };

        // Act
        var result = await controller.CreateLoanAsync(dto);

        // Assert
        Assert.IsType<ConflictObjectResult>(result);
    }

    [Fact]
    public async Task GivenMismatchedIds_WhenUpdateBookCalled_ThenReturnsBadRequest()
    {
        // Arrange
        var repo = Substitute.For<ILibrarianRepository>();
        var controller = new LibrarianController(repo);

        var dto = new BookDto
        {
            BookId = 2,
            Title = "Test",
            Author = "Test"
        };

        // Act
        var result = await controller.UpdateBookAsync(1, dto);

        // Assert
        Assert.IsType<BadRequestObjectResult>(result);
    }
}
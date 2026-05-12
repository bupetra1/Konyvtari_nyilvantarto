using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class CreateLoanDtoTests
{
    [Fact]
    public void GivenNewLoan_WhenLoanDateIsChecked_ThenReturnsTodayDate()
    {
        // Arrange
        var loan = new CreateLoanDto
        {
            ReaderId = 1,
            BookId = 1,
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7))
        };

        // Act
        var result = loan.LoanDate;

        // Assert
        Assert.Equal(DateOnly.FromDateTime(DateTime.Now), result);
    }

    [Fact]
    public void GivenValidLoan_WhenPropertiesAreChecked_ThenValuesAreCorrect()
    {
        // Arrange
        var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7));

        var loan = new CreateLoanDto
        {
            ReaderId = 1,
            BookId = 2,
            DueDate = dueDate
        };

        // Act
        var readerId = loan.ReaderId;
        var bookId = loan.BookId;
        var resultDueDate = loan.DueDate;

        // Assert
        Assert.Equal(1, readerId);
        Assert.Equal(2, bookId);
        Assert.Equal(dueDate, resultDueDate);
    }
}
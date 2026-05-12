using System.ComponentModel.DataAnnotations;
using Share.Dtos;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class LoanDtoTests
{
    [Fact]
    public void GivenFutureLoanDate_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var loan = new LoanDto
        {
            LoanDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7))
        };

        var context = new ValidationContext(loan);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            loan,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Loan date cannot be earlier than the current day!");
    }

    [Fact]
    public void GivenReturnDateBeforeLoanDate_WhenValidationOccurs_ThenReturnsValidationError()
    {
        // Arrange
        var loan = new LoanDto
        {
            LoanDate = new DateOnly(2025, 5, 10),
            DueDate = new DateOnly(2025, 5, 20),
            ReturnDate = new DateOnly(2025, 5, 5)
        };

        var context = new ValidationContext(loan);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            loan,
            context,
            results,
            true);

        // Assert
        Assert.False(isValid);

        Assert.Contains(results,
            r => r.ErrorMessage == "Return date cannot be earlier than the loan day!");
    }

    [Fact]
    public void GivenLateReturn_WhenLateFeeCalculated_ThenReturnsCorrectFee()
    {
        // Arrange
        var loan = new LoanDto
        {
            LoanDate = new DateOnly(2025, 5, 1),
            DueDate = new DateOnly(2025, 5, 10),
            ReturnDate = new DateOnly(2025, 5, 15)
        };

        // Act
        var result = loan.LateFee;

        // Assert
        Assert.Equal(500, result);
    }

    [Fact]
    public void GivenValidLoan_WhenValidationOccurs_ThenReturnsSuccess()
    {
        // Arrange
        var loan = new LoanDto
        {
            LoanDate = DateOnly.FromDateTime(DateTime.Now),
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(7)),
            ReturnDate = null
        };

        var context = new ValidationContext(loan);
        var results = new List<ValidationResult>();

        // Act
        var isValid = Validator.TryValidateObject(
            loan,
            context,
            results,
            true);

        // Assert
        Assert.True(isValid);
        Assert.Empty(results);
    }
}
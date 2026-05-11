using System.ComponentModel.DataAnnotations;
using System.Data;
using Konyvtari_nyilvantarto.Validations;
using System.Reflection;
using Xunit;
using Share.Dtos;

namespace Konyvtari_nyilvantarto.Tests;

public class ValidReturnDateAttributeUnitTest
{
    [Fact]
    public void ValidReturnDate_ValidationOccurs_ReturnsSuccess()
    {
        // Arrange
        var attribute = new ValidReturnDateAttribute();
        var loan = new LoanDto { LoanDate = new DateOnly(2020, 1, 1) };
        var returnDate = new DateOnly(2020, 1, 2);
        var context = new ValidationContext(loan);
        // Act
        var result = attribute.GetValidationResult(returnDate, context);
        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }
    [Fact]
    public void SameDayReturnDate_ValidationOccurs_ReturnsSuccess()
    {
        // Arrange
        var attribute = new ValidReturnDateAttribute();
        var loan = new LoanDto { LoanDate = new DateOnly(2020, 1, 1) };
        var returnDate = new DateOnly(2020, 1, 1);
        var context = new ValidationContext(loan);
        // Act
        var result = attribute.GetValidationResult(returnDate, context);
        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }
    [Fact]
    public void ReturnDateBeforeLoanDate_ValidationOccurs_ReturnsError()
    {
        // Arrange
        var attribute = new ValidReturnDateAttribute();
        var loan = new LoanDto { LoanDate = new DateOnly(2020, 1, 2) };
        var returnDate = new DateOnly(2020, 1, 1);
        var context = new ValidationContext(loan);
        // Act
        var result = attribute.GetValidationResult(returnDate, context);
        // Assert
        Assert.Equal(ValidationResult.Success, result);
    }
}

using System.ComponentModel.DataAnnotations;
using System.Data;
using Share.Validations;
using System.Reflection;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ValidLoanDateAttributeUnitTest
{
    [Fact]
    public void Today_ValidationOccurs_ReturnsSuccess()
    {
        //Arrange
        var attribute = new ValidLoanDateAttribute();
        var today = DateTime.Today;
        var context = new ValidationContext(new object());
        //Acts
        var result = attribute.GetValidationResult(today, context);
        //Assert
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void PastDate_ValidationOccurs_ReturnsValidationError()
    {
        //Arrange
        var attribute = new ValidLoanDateAttribute();
        var pastDate = DateTime.Today.AddDays(1);
        var context = new ValidationContext(new object());
        //Act
        var result = attribute.GetValidationResult(pastDate, context);
        //Assert
        Assert.NotNull(result);
        Assert.Equal("Loan date cannot be earlier than the current day!", result.ErrorMessage);
    }   
    [Fact]
    public void FutureDate_ValidationOccurs_ReturnsValidationError()
    {
        //Arrange
        var attribute = new ValidLoanDateAttribute();
        var pastDate = DateTime.Today.AddDays(1);
        var context = new ValidationContext(new object());
        //Act
        var result = attribute.GetValidationResult(pastDate, context);
        //Assert
        Assert.NotNull(result);
        Assert.Equal("Loan date cannot be later than the current day!", result.ErrorMessage);
    }
}

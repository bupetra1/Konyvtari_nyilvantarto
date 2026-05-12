using Share.Logic;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class LateFeeCalculatorTests
{
    [Fact]
    public void GivenFiveDaysLate_WhenCalculatingFee_ThenReturns500()
    {
        // Arrange
        var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-5));
        DateOnly? returnDate = null;

        // Act
        var result = LateFeeCalculator.CalculateLateFee(dueDate, returnDate);

        // Assert
        Assert.Equal(500, result);
    }

    [Fact]
    public void GivenTwelveDaysLate_WhenCalculatingFee_ThenReturns2400()
    {
        // Arrange
        var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-12));
        DateOnly? returnDate = null;

        // Act
        var result = LateFeeCalculator.CalculateLateFee(dueDate, returnDate);

        // Assert
        Assert.Equal(2400, result);
    }

    [Fact]
    public void GivenTwentyDaysLate_WhenCalculatingFee_ThenReturns6000()
    {
        // Arrange
        var dueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-20));
        DateOnly? returnDate = null;

        // Act
        var result = LateFeeCalculator.CalculateLateFee(dueDate, returnDate);

        // Assert
        Assert.Equal(6000, result);
    }

    [Fact]
    public void GivenReturnedOnTime_WhenCalculatingFee_ThenReturnsZero()
    {
        // Arrange
        var dueDate = DateOnly.FromDateTime(DateTime.Now);
        DateOnly? returnDate = DateOnly.FromDateTime(DateTime.Now);

        // Act
        var result = LateFeeCalculator.CalculateLateFee(dueDate, returnDate);

        // Assert
        Assert.Equal(0, result);
    }
}
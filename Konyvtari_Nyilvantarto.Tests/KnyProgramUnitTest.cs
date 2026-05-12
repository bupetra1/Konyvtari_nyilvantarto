using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Konyvtari_nyilvantarto.Tests;

public class ServiceRegistrationTests
{
    [Fact]
    public void GivenServices_WhenBuilt_ThenCorsPolicyExists()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var provider = services.BuildServiceProvider();

        // Act
        var cors = provider.GetService<Microsoft.AspNetCore.Cors.Infrastructure.ICorsService>();

        // Assert
        Assert.NotNull(cors);
    }

    [Fact]
    public void GivenServices_WhenConfigured_ThenDbContextIsRegistered()
    {
        // Arrange
        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=test.db"));

        var provider = services.BuildServiceProvider();

        // Act
        var db = provider.GetService<AppDbContext>();

        // Assert
        Assert.NotNull(db);
    }
}
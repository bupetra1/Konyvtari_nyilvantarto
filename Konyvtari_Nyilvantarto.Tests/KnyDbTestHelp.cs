using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Konyvtari_nyilvantarto;

public class DbTestHelper
{
    public static AppDbContext CreateDb()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(connection)
            .Options;

        var context = new AppDbContext(options);
        context.Database.EnsureCreated();

        return context;
    }
}
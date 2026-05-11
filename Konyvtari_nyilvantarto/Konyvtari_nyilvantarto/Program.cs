using Konyvtari_nyilvantarto;
using Konyvtari_nyilvantarto.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=library.db"));
builder.Services.AddSwaggerGen(options =>
{
    // Általános leírás beállítása
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Könyvtári Nyilvántartó API",
        Version = "1.0.0",
        Description = "Ez az API a könyvtárosok és olvasók adatait kezeli. Használható kölcsönzések követésére és adminisztrációra.",
    });

    // XML dokumentáció beolvasása (a metódusok feletti /// kommentekhez)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    // Csak akkor próbálja beolvasni, ha létezik a fájl (megelőzi az összeomlást)
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddScoped<ILibrarianRepository, LibrarianRepository>();
builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated(); 
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

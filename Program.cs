using WebApplication2.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});;
builder.Services.AddOpenApi();

var connectionString = Environment.GetEnvironmentVariable("DBCONNECTION");
builder.Services.AddControllers();
builder.Services.AddDbContext<MyAppContext>(options => options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention());

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
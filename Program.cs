using Supabase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using WebApplication2.Controllers;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Controllers
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddOpenApi();

// 2. Read Environment Variables
var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
var connectionString = Environment.GetEnvironmentVariable("DBCONNECTION");

// 3. Initialize Supabase
var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};
var supabase = new Supabase.Client(url, key, options);
await supabase.InitializeAsync();

// 4. ✅ FIX: Register the client so your Controllers can use it!
builder.Services.AddSingleton<Supabase.Client>(supabase);
builder.Services.AddSingleton<string>(connectionString);

var app = builder.Build();

// 5. Configure Pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// 6. ✅ FIX: Map your Controllers to routes!
app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using MyLibrary.Extensions;
using MyLibrary.Interfaces;
using MyLibrary.Models;
using MyLibrary.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Entity Framework
builder.Services.AddDbContext<AdventureWorksContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb")));

// Register repositories based on configuration
var useDatabase = builder.Configuration.GetValue<bool>("UseDatabase");
if (useDatabase)
{
    builder.Services.AddScoped<IBookRepository, SqlBookRepository>();
}
else
{
    builder.Services.AddScoped<IBookRepository, JsonBookRepository>();
}

// Register combined repository
builder.Services.AddScoped<ICombinedBookRepository, CombinedBookRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.SeedDatabaseAsync();
}

app.Run();

using Microsoft.EntityFrameworkCore;
using PersonRecommendationApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlite(
      builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IPersonRepository, DbPersonRepository>();
builder.Services.AddScoped<Initializer>();

var app = builder.Build();
await SeedDataAsync(app);

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


app.Run();

static async Task SeedDataAsync(WebApplication app)
{
	using var scope = app.Services.CreateScope();
	var services = scope.ServiceProvider;
	try
	{
		var initializer = services.GetRequiredService<Initializer>();
		await initializer.SeedDatabaseAsync();
	}
	catch (Exception ex)
	{
		var logger = services.GetRequiredService<ILogger<Program>>();
		logger.LogError("An error occurred while seeding the database: {Message}", ex.Message);
	}
}


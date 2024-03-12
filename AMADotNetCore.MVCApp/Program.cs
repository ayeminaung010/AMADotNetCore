using AMADotNetCore.MVCApp;
using AMADotNetCore.MVCApp.EFCoreDbContext;
using Microsoft.EntityFrameworkCore;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},ServiceLifetime.Transient,ServiceLifetime.Transient); //db connection service add

//builder.Services.AddScoped<HttpClient>();

builder.Services.AddScoped(n =>
{
    HttpClient httpClient = new HttpClient()
    {
        BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!)
    };
    return httpClient;
});

builder.Services.AddScoped(n =>
{
    RestClient restClient = new RestClient(builder.Configuration.GetSection("ApiUrl").Value!);
	return restClient;
});


builder.Services
	.AddRefitClient<IBlogApi>()
	.ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiUrl").Value!));

var app = builder.Build();

// Check database connection
try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.EnsureCreated(); // This will throw an exception if the connection fails
    }
    Console.WriteLine("Database connection successful.");
}
catch (Exception ex)
{
    Console.WriteLine($"Database connection failed. Error: {ex.Message}");
    // Handle the exception or take appropriate action
    throw;
}
//end checking

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

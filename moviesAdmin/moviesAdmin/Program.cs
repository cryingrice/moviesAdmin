using Auth0.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using MoviesAdmin;
using MoviesAdmin.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"];
        options.ClientId = builder.Configuration["Auth0:ClientId"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
    });
    
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<AzureBlobService>();
// Register EF Core DbContext
builder.Services.AddDbContext<MoviesAdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesAdminContext")));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

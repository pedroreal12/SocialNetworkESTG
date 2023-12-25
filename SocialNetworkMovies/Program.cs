using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetworkMovies.Data;
using SocialNetworkMovies.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SocialNetworkMoviesContextConnection") ?? throw new InvalidOperationException("Connection string 'SocialNetworkMoviesContextConnection' not found.");

builder.Services.AddDbContext<SocialNetworkMoviesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SocialNetworkMoviesIdentityConnection")));

builder.Services.AddDbContext<SocialNetworkMoviesContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<SocialNetworkMoviesUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SocialNetworkMoviesContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapRazorPages();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

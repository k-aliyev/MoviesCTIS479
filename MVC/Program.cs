using Business.Services;
using DataAccess.Contexts;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVC.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<Db>(options => options.UseSqlServer("server=(localdb)\\mssqllocaldb;database=MoviesDb;trusted_connection=true;"));

builder.Services.AddScoped<IDirectorService, DirectorService>();
builder.Services.AddScoped<IGenraService, GenraService>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddSession(config =>
{
    config.IdleTimeout = TimeSpan.FromMinutes(30); 
                                                   
    config.IOTimeout = Timeout.InfiniteTimeSpan; 
});

var section = builder.Configuration.GetSection(nameof(MVC.Settings.AppSettings));
section.Bind(new MVC.Settings.AppSettings());

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Users/Login";
        config.AccessDeniedPath = "/Users/AccessDenied";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(AppSettings.CookieExpirationInMinutes);
        config.SlidingExpiration = true;
    });

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(name: "register",
    pattern: "register",
    defaults: new { controller = "Users", action = "Create" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();

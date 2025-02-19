using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;


var builder = WebApplication.CreateBuilder(args);

// Add DbContext to the service container.
builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApplication1Context") ??
    throw new InvalidOperationException("Connection string 'WebApplication1Context' not found.")));

// Add Identity services to the container.
builder.Services.AddIdentity<UserModel, IdentityRole>()
    .AddEntityFrameworkStores<WebApplication1Context>()
    .AddDefaultTokenProviders();

// Add MVC services.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

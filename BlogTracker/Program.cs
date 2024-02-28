using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Session;
using BlogTracker.Data;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogTrackerdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogTrackerdbContext") ?? throw new InvalidOperationException("Connection string 'BlogTrackerdbContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add this line to add session support
builder.Services.AddDistributedMemoryCache(); // Required to use session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // You can set Time 
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();
// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BlogInfoes}/{action=Index}/{id?}");

app.Run();

using FujitsuWebApp.Helper;
using FujitsuWebApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbFujitsuContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbFujitsuContext") ?? throw new InvalidOperationException("Connection string 'DbFujitsuContext' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped(typeof(IExcelHelperService<>), typeof(ExcelHelperService<>));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TbMSuppliers}/{action=Index}/{id?}");

app.Run();

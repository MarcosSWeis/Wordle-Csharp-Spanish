using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Wordle.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddSingleton<LoadWords>();
builder.Services.AddSingleton<KeyBoard>();
builder.Services.AddSingleton<ITempDataProvider,CookieTempDataProvider>();
builder.Services.AddSession();

builder.Services.AddCors(options =>
{
    options.AddPolicy("algodeCors",app => app.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseCors("algodeCors");
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

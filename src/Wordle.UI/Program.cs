using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using System.Web.Services.Description;
using Wordle.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSession();
builder.Services.AddSingleton<SettingsGame>();
//Seweet Alet 2
builder.Services.AddSweetAlert2(optrion => optrion.SetThemeForColorSchemePreference(ColorScheme.Dark,SweetAlertTheme.Dark));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

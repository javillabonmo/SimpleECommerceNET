// <copyright file="Program.cs" company="N/A">
// Copyright (c) N/A. All rights reserved.
// </copyright>
// <author> javillabonmo </author>

using System.Globalization;
using System.Runtime.InteropServices;

using Application.Services;
using Application.Services.Interfaces;

using Infraestructure.Interfaces;
using Infraestructure.Persistence;
using Infraestructure.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// cultura regional (formato de fechas, moneda, etc.)
var cultureInfo = new CultureInfo("es-CO");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

//app.UseAuthorization();

//app.MapStaticAssets();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}")
   .WithStaticAssets();

app.Run();

using freezer;
using freezer.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using freezer.DAL.Entities;
using freezer.DAL;
using FreezerWebPages.Services;

var builder = WebApplication.CreateBuilder(args);
var host = WebApplication.CreateBuilder(args);
host.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5002);});

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<FoodDataService>();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<FreezerContext>();

builder.Services.AddTransient<IFreezerLogic, FreezerLogic>();
builder.Services.AddTransient<IFoodItemRepository, FoodItemRepository>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MapRazorPages(); 

app.Run();
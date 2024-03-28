using freezer;
using freezer.Logic;
using Microsoft.EntityFrameworkCore;
using IronBarCode;
using Microsoft.AspNetCore.Builder;
using freezer.DAL.Entities;
using freezer.DAL;


IronBarCode.License.LicenseKey = "IRONSUITE.MATTHEW.SALYER.YAHOO.COM.16019-E0A6ABA8CB-GMRFRJO35KQIL2-HDFEPAFMNFRZ-H4IXLW2MKIRI-DRJ25Y6O3TFZ-PZLSMLJYE4RS-G4P2I7SMT3DX-FHVAAH-TZGSTWSZWOCMEA-DEPLOYMENT.TRIAL-PBZGYP.TRIAL.EXPIRES.13.APR.2024";
bool is_licensed = IronBarCode.License.IsLicensed;

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
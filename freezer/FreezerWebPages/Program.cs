using freezer;
using freezer.DAL.Migrations;
using freezer.Logic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<FreezerContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FreezerContext")));

builder.Services.AddScoped<IFreezerLogic, FreezerLogic>();
builder.Services.AddScoped<IFoodItemRepository, FoodItemRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();

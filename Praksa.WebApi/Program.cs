using Praksa.Common;
using Praksa.Repository;
using Praksa.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//DI builder built-in
builder.Services.AddTransient<IFootballClubService, FootballClubService>();
builder.Services.AddTransient<IFootballClubRepository, FootballClubRepository>();
builder.Services.AddTransient<IFootballPlayerService, FootballPlayerService>();
builder.Services.AddTransient<IFootballPlayerRepository, FootballPlayerRepository>();
builder.Services.AddTransient<DatabaseHelper>();

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

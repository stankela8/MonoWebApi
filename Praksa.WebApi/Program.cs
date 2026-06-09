using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Praksa.Common;
using Praksa.Repository;
using Praksa.Repository.Common;
using Praksa.Service;
using Praksa.Service.Common;
using Praksa.WebApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<FootballClubUpsertRequest, FootballClub>();
    cfg.CreateMap<FootballClub, FootballClubResponse>();
}, NullLoggerFactory.Instance);

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//DI builder built-in
/*
builder.Services.AddTransient<IFootballClubService, FootballClubService>();
builder.Services.AddTransient<IFootballClubRepository, FootballClubRepository>();
builder.Services.AddTransient<IFootballPlayerService, FootballPlayerService>();
builder.Services.AddTransient<IFootballPlayerRepository, FootballPlayerRepository>();
builder.Services.AddTransient<DatabaseHelper>();
*/

//DI builder Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterType<FootballClubService>().As<IFootballClubService>().InstancePerDependency();

    containerBuilder.RegisterType<FootballClubRepository>().As<IFootballClubRepository>().InstancePerDependency();

    containerBuilder.RegisterType<FootballPlayerService>().As<IFootballPlayerService>().InstancePerDependency();

    containerBuilder.RegisterType<FootballPlayerRepository>().As<IFootballPlayerRepository>().InstancePerDependency();

    containerBuilder.RegisterType<DatabaseHelper>().AsSelf().InstancePerDependency();
});


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

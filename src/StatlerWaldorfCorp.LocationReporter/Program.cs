using Microsoft.Extensions.DependencyInjection;
using StatlerWaldorfCorp.LocationReporter.Events;
using StatlerWaldorfCorp.LocationReporter.Models;
using StatlerWaldorfCorp.LocationReporter.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Services.Configure<AMQPOptions>(builder.Configuration.GetSection("amqp"));
builder.Services.Configure<TeamServiceOptions>(builder.Configuration.GetSection("teamservice"));

builder.Services.AddSingleton<IEventEmitter, AMQPEventEmitter>();
builder.Services.AddSingleton<ICommandEventConverter, CommandEventConverter>();
builder.Services.AddSingleton<ITeamServiceClient, TeamServiceHttpClient>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();


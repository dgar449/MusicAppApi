using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MusicAppApi.AutoMapper;
using MusicAppApi.Data.Models;
using MusicAppApi.Data.Models.Context;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISongRepo, DbSongRepo>();

builder.Services.AddDbContextPool<AppDbContext>(
    options => options.UseSqlServer(configuration.GetConnectionString("DBConn")));


builder.Services.AddAutoMapper(typeof(MusicMappingProfile).Assembly);

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

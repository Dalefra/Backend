using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddSingleton<IPeopleService, PeopleService>();

builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("people");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("RandomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("RandomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("RandomTransient");

builder.Services.AddScoped<IPostsService, PostsService>();

// HttpClient servicio jsonPlaceholder
builder.Services.AddHttpClient<IPostsService, PostsService>(c => 
{
    c.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]);
});

// Entity Framework
builder.Services.AddDbContext<StoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//validadores
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();

builder.Services.AddControllers();
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

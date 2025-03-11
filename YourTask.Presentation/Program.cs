using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using YourTask.Domain.Validations;
using YourTask.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using YourTask.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();
builder.Services.AddTransient<TarefaValidator>();

builder.Services.AddDbContext<YourTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Validação (FluentValidation)
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<TarefaValidator>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

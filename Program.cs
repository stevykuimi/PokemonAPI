using Microsoft.EntityFrameworkCore;
using PokemonAPI.Models;
using PokemonAPI.Features.Queries.GetPokemon;
using MediatR;
using FluentAssertions.Common;
using System.Reflection;
using PokemonAPI.Interfaces;

namespace PokemonAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<CSVService>(new CSVService("C:\\Users\\skuimi\\source\\repos\\PokemonAPI\\pokemon.csv"));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        builder.Services.AddTransient<GetPokemonHandler>();

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
    }   

}

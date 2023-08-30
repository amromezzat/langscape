using Application.Extensions;
using Persistence.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using Persistence.Seeds;
using System;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Infrastructure.Security.Seeds;
using Infrastructure.Security.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

MigrateDatabase();

app.Run();

async void MigrateDatabase()
{
    var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;

    try
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        var unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        await context.Database.MigrateAsync();
        await FlashCardSetsSeed.SeedFlashCardSets(unitOfWork);
        await unitOfWork.Save(CancellationToken.None);

        var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        await UsersSeed.SeedUsers(userManager);
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during migration");
    }
}
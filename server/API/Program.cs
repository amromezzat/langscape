using Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories;
using System;
using System.Threading;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Infrastructure.Security.Seeds;
using Infrastructure.Security.Extensions;
using Persistence.Extension;
using Persistence.Contexts;
using Persistence.Seeds;
using API.Extensions;
using APIExtensions = API.Extensions.ServiceCollectionExtensions;
using Infrastructure.Extensions;
using API.Common.ErrorHandling.Middlewares.Impl;
using System.Threading.Tasks;
using ILogger = Infrastructure.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
builder.Services.AddInfrastructureLayer(builder.Configuration);
builder.Services.AddApiLayer();
builder.Services.AddExternalLogging();
builder.Services.AddControllers(config => {
    config.ReturnHttpNotAcceptable = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if(app.Environment.IsProduction())
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseCors(APIExtensions.CorsPolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
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
    catch (Exception exception)
    {
        var logger = serviceProvider.GetRequiredService<ILogger>();
        logger.LogError($"An error occurred during migration: {exception}");
    }
}
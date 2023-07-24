using SharedRecipe.Api.Filters;
using SharedRecipe.Application;
using SharedRecipe.Application.Services.Automapper;
using SharedRecipe.Domain.Extension;
using SharedRecipe.Infrastructure;
using SharedRecipe.Infrastructure.Migrations;
using SharedRecipe.Infrastructure.RepositoryAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutomapperConfig());
}).CreateMapper());

builder.Services.AddScoped<UserAuthorization>();

var app = builder.Build();

void ValidateDatabaseIntegrity()
{
    using IServiceScope serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

    using SharedRecipeContext sharedRecipeContext = serviceScope.ServiceProvider.GetService<SharedRecipeContext>();

    bool? databaseInMemory = sharedRecipeContext?.Database?.ProviderName?.Equals("Microsoft.EntityFrameworkCore.InMemory");

    if (!databaseInMemory.HasValue || !databaseInMemory.Value)
    {
        string connection = builder.Configuration.GetConnectionStrings();
        string nameDatabase = builder.Configuration.GetNameDatabase();

        Database.CreateDatabase(connection, nameDatabase);

        app.MigrateDatabase();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

ValidateDatabaseIntegrity();

app.Run();

public partial class Program { };
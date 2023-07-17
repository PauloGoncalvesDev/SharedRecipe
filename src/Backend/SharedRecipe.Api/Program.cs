using SharedRecipe.Api.Filters;
using SharedRecipe.Domain.Extension;
using SharedRecipe.Infrastructure;
using SharedRecipe.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration);
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilters)));

var app = builder.Build();

void ValidateDatabaseIntegrity()
{
    string connection = builder.Configuration.GetConnectionStrings();
    string nameDatabase = builder.Configuration.GetNameDatabase();

    Database.CreateDatabase(connection, nameDatabase);

    app.MigrateDatabase();
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
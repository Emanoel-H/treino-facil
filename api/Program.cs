using Api.Data;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

var senha = Env.GetString("DB_PASSWORD") 
            ?? throw new InvalidOperationException("DB_PASSWORD environment variable is not set");

var usuario = Env.GetString("DB_USER") 
            ?? throw new InvalidOperationException("DB_USER environment variable is not set");

var db_name = Env.GetString("DB_NAME") 
              ?? throw new InvalidOperationException("DB_NAME environment variable is not set");

var connectionString = new SqlConnectionStringBuilder
{
    DataSource = $"{Env.GetString("DB_HOST", "localhost")}, {Env.GetString("DB_PORT", "1433")}",
    InitialCatalog = db_name,
    UserID = usuario,
    Password = senha,
    TrustServerCertificate = true
}.ConnectionString;

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

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

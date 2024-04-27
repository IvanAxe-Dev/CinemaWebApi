using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.Domain.Entities;
using Cinema.Infrastructure.MapsterConfiguration;
using Cinema.Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddMapster();
MapsterConfig.Configure();

builder.Services.AddDbContext<SqlcinemadbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(SqlcinemadbContext)));
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

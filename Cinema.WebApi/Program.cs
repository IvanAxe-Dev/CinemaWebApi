using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.ServiceContracts;
using Cinema.Core.Services;
using Cinema.Infrastructure.MapsterConfiguration;
using Cinema.Infrastructure.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient(typeof(IService<>), typeof(Service<>));

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

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = true;

        options.User.RequireUniqueEmail = true;
    })
    .AddEntityFrameworkStores<SqlcinemadbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, SqlcinemadbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, SqlcinemadbContext, Guid>>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;
using RestWithASPNETErudio.Services.Implementations;
using RestWithAspNetUdemy.Business;
using RestWithAspNetUdemy.Business.Implementations;
using RestWithAspNetUdemy.Configurations;
using RestWithAspNetUdemy.Hypermedia.Enricher;
using RestWithAspNetUdemy.Hypermedia.Filters;
using RestWithAspNetUdemy.Model.Context;
using RestWithAspNetUdemy.Repository;
using RestWithAspNetUdemy.Repository.Generic;
using RestWithAspNetUdemy.Services;
using RestWithAspNetUdemy.Services.Implementations;
using Serilog;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var appName = "REST API'S";
var appVersion = "v1";
// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
        builder.Configuration.GetSection("TokenConfigurations")
    ).Configure(tokenConfigurations);


builder.Services.AddSingleton(tokenConfigurations);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfigurations.Issuer,
            ValidAudience = tokenConfigurations.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfigurations.Secret))
        };
    });
builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser().Build());
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();

}));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(appVersion,
        new OpenApiInfo
        {
            Title = appName,
            Version = appVersion,
            Description = "API'S from 0",
            Contact = new OpenApiContact
            {
                Name = "Igor Marques",
                Url = new Uri("https://github.com/igormarques2502")
            }
        });
});

var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];
builder.Services.AddDbContext<MySQLContext>(options => options.UseMySql(
    connection,
    new MySqlServerVersion(new Version(8, 2, 0))));

if (builder.Environment.IsDevelopment())
{
    MigrateDatabase(connection);
}

builder.Services.AddMvc(options =>
{
    options.RespectBrowserAcceptHeader = true;
    options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
    options.FormatterMappings.SetMediaTypeMappingForFormat("json", "application/json");
})
    .AddXmlSerializerFormatters();

var filterOptions = new HyperMediaFilterOptions();
filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
filterOptions.ContentResponseEnricherList.Add(new BookEnricher());
builder.Services.AddSingleton(filterOptions);

builder.Services.AddApiVersioning();

//Dependency Injection
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
builder.Services.AddScoped<IBookBusiness, BookBusiness>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IFileBusiness, FileBusiness>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("./v1/swagger.json",
        $"{appName} - {appVersion}");
});

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute("DefaultApi", "{controller=values}/v{version=apiVersion}/{id?}");

app.Run();
void MigrateDatabase(string? connection)
{
    try
    {
        var evolveConnection = new MySqlConnection(connection);
        var evolve = new Evolve(evolveConnection, Log.Information)
        {
            Locations = new List<string> { "db/migrations", "db/dataset" },
            IsEraseDisabled = true,
        };
        evolve.Migrate();

    }
    catch (Exception ex)
    {
        Log.Error("Database migration failed", ex.Message);
        throw;
    }
}
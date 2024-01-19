using Croptor.Api.Attributes;
using Croptor.Api.Common.Mapping;
using Croptor.Api.Services;
using Croptor.Application;
using Croptor.Application.Common.Interfaces;
using Croptor.Domain.Users.ValueObjects;
using Croptor.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddInfrastructure(configuration);
services.AddApplication();

services.AddMapping();

// services.AddIdentity<User, IdentityRole<Guid>>()
//     .AddEntityFrameworkStores<CroptorDbContext>()
//     .AddDefaultTokenProviders();

services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilterAttribute>();
});

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = configuration["JwtSetting:Authority"];
        options.TokenValidationParameters.ValidateAudience = false;
    });

services.AddAuthorization(options =>
{
    options.AddPolicy("ProPlan", policy =>
    {
        policy.RequireClaim("plan", PlanType.Pro.ToString(), PlanType.Admin.ToString());
    });
    options.AddPolicy("Admin", policy =>
    {
        policy.RequireClaim("plan", PlanType.Admin.ToString());
    });
});

services.AddCors(options =>
{
    options.AddPolicy("Development", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://croptor.com")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

services.AddHttpContextAccessor();
services.AddScoped<IUserProvider, UserProvider>();

services.AddHostedService<ArchiveRemover>();

services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 200000000; // Adjust the limit as needed
});

WebApplication app = builder.Build();
bool isDevelopment = app.Environment.IsDevelopment();

if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

string corsPolicy = isDevelopment ? "Development" : "Production";
app.UseCors(corsPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

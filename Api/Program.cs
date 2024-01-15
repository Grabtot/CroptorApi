using Croptor.Api.Attributes;
using Croptor.Api.Common.Mapping;
using Croptor.Api.Services;
using Croptor.Application;
using Croptor.Application.Common.Interfaces;
using Croptor.Domain.Users.ValueObjects;
using Croptor.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;
ConfigurationManager configuration = builder.Configuration;

services.AddInfrastructure(configuration);
services.AddApplication();

services.AddMapping();

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
        options.Audience = configuration["JwtSetting:Audience"];
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
});

services.AddHttpContextAccessor();
services.AddScoped<IUserProvider, UserProvider>();


WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Development");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

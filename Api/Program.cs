using Croptor.Api.Attributes;
using Croptor.Api.Common.Mapping;
using Croptor.Api.Services;
using Croptor.Application;
using Croptor.Application.Common.Interfaces;
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

services.AddHttpContextAccessor();
services.AddScoped<IUserProvider, UserProvider>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

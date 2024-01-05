using Croptor.Api.Attributes;
using Croptor.Api.Common.Mapping;
using Croptor.Api.Services;
using Croptor.Application;
using Croptor.Application.Common.Interfaces;
using Croptor.Domain.Users;
using Croptor.Infrastructure;
using Croptor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<CroptorDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddMapping();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilterAttribute>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();

builder.Services.AddScoped<IUserProvider, UserProvider>();

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

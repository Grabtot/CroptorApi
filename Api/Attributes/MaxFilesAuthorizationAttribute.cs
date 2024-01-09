using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Croptor.Api.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class MaxFilesAuthorizationAttribute : Attribute, IAsyncAuthorizationFilter
{
    private readonly int maxFiles;

    public MaxFilesAuthorizationAttribute(int maxFiles)
    {
        this.maxFiles = maxFiles;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var files = context.HttpContext.Request.Form.Files;

        if (files.Count > maxFiles)
        {
            // Extract the user's plan claim from the JWT token
            var userPlan = GetUserPlanFromToken(context.HttpContext.User);

            // Apply authorization logic based on the user's plan
            if (userPlan != "Pro")
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }

    // Extract the user's plan claim from the JWT token
    private string GetUserPlanFromToken(ClaimsPrincipal user)
    {
        var userPlanClaim = user.FindFirst("plan");
        return userPlanClaim?.Value;
    }
}
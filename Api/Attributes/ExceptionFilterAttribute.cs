﻿using Croptor.Domain.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Croptor.Api.Attributes
{
    public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilterAttribute> _logger;

        public ExceptionFilterAttribute(ILogger<ExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            Exception exception = context.Exception;

            _logger.LogError(exception, message: exception.Message);

            if (exception is ValidationException validationException)
            {
                context.Result = ValidationError(context, validationException);
                context.ExceptionHandled = true;

                await base.OnExceptionAsync(context);
                return;
            }

            (int code, string message) = exception switch
            {
                UserNotAuthenticatedException => (StatusCodes.Status401Unauthorized, exception.Message),
                NotImplementedException => (StatusCodes.Status501NotImplemented, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An error occurred while processing your request")
            };

            ProblemDetails problemDetails = new()
            {
                Status = code,
                Title = message
            };
            context.Result = new ObjectResult(problemDetails);

            context.ExceptionHandled = true;

            await base.OnExceptionAsync(context);
        }

        private static ObjectResult ValidationError(ExceptionContext context, ValidationException validationException)
        {
            ModelStateDictionary modelState = new();

            foreach (ValidationFailure? error in validationException.Errors)
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            ValidationProblemDetails details = new(modelState)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
                Detail = "See the errors property for details.",
                Instance = context.HttpContext.Request.Path
            };
            return new ObjectResult(details);
        }
    }
}

using FluentValidation;
using MediatR;

namespace Croptor.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IValidator<TRequest>? _validator = validator;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is not null)
                await _validator.ValidateAndThrowAsync(request, cancellationToken);

            return await next();
        }
    }
}

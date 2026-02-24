using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Application.Mediator;

public sealed class ApplicationMediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    public ApplicationMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        var handler = _serviceProvider.GetRequiredService(handlerType);
        var handleMethod = handlerType.GetMethod(nameof(IRequestHandler<IRequest<TResponse>, TResponse>.Handle));

        if (handleMethod is null)
        {
            throw new InvalidOperationException($"Handle method not found for {handlerType.Name}.");
        }

        return (Task<TResponse>)handleMethod.Invoke(handler, new object[] { request, cancellationToken })!;
    }
}

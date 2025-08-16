using FluentValidation;
using Silo.API.Helpers.Mapper;

namespace Silo.API.Common.Controller;

public class BaseControllerParams<TRequest>
{
    public IMediator Mediator { get; }
    public IValidator<TRequest>? Validator { get; }
    public IMapperHelper Mapper { get; }

    public BaseControllerParams(
        IMediator mediator,
        IMapperHelper mapper,
        IServiceProvider serviceProvider)
    {
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        Validator = serviceProvider.GetService<IValidator<TRequest>>();
    }
}
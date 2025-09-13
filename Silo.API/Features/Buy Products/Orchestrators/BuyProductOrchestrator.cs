using Silo.API.Common.Request;
using Silo.API.Features.Buy_Products.Commands;
using Silo.API.Features.Buy_Products.DTOs;

namespace Silo.API.Features.Buy_Products.Orchestrators;

public record BuyProductOrchestrator(int ProductId, int Quantity) : IRequest<ResultDTO<BuyProductDTO>>;

public class BuyProductOrchestratorHandler(RequestHandlerBaseParameters requestHandlerBaseParameters) : RequestHandlerBase<BuyProductOrchestrator, ResultDTO<BuyProductDTO>>(requestHandlerBaseParameters)
{
    public override async Task<ResultDTO<BuyProductDTO>> Handle(BuyProductOrchestrator request, CancellationToken cancellationToken)
    {
        var buyProductCommandResult = await _mediator.Send(new BuyProductCommand(request.ProductId, request.Quantity));

        if (!buyProductCommandResult.IsSuccess)
            return ResultDTO<BuyProductDTO>.Failure(message: buyProductCommandResult.Message,
                statusCode: buyProductCommandResult.StatusCode, errors: buyProductCommandResult.Errors);


        return ResultDTO<BuyProductDTO>.Success(message: buyProductCommandResult.Message, statusCode: buyProductCommandResult.StatusCode);
    }
}
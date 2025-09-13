using Silo.API.Common.Controller;
using Silo.API.Features.Buy_Products.DTOs;
using Silo.API.Features.Buy_Products.Orchestrators;

namespace Silo.API.Features.Endpoints.Buy_Products;

public class BuyProductEndpoint(BaseControllerParams<BuyProductRequest> baseControllerParams) : BaseController<BuyProductRequest, BuyProductResponse>(baseControllerParams)
{
    [HttpPost("products/buy")]
    [ProducesResponseType(typeof(ApiResponse<BuyProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<BuyProductResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<BuyProductResponse>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<BuyProductResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<ApiResponse<BuyProductResponse>> BuyProductAsync([FromBody] BuyProductRequest request, CancellationToken cancellationToken)
    {
        var validateRequest = await ValidateRequestAsync(request);
        if (!validateRequest.IsSuccess)
        {
            return validateRequest;
        }


        var result = await _mediator.Send(new BuyProductOrchestrator(request.productId, request.quantity), cancellationToken);

        var response = HandleResult<BuyProductDTO, BuyProductResponse>(result);

        return response;
    }
}
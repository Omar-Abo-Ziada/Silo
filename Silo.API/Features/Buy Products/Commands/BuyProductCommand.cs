using Silo.API.Common.Request;
using Silo.API.Features.Buy_Products.DTOs;
using Silo.API.Presistance.Contexts.Repositories.Common;

namespace Silo.API.Features.Buy_Products.Commands;

public record BuyProductCommand(int ProductId, int Quantity) : IRequest<
    ResultDTO<bool>>;

public class BuyProductCommandHandler(RequestHandlerBaseParameters requestHandlerBaseParameters, IRepository<Product> productsRepository) : RequestHandlerBase<BuyProductCommand, ResultDTO<bool>>(requestHandlerBaseParameters)
{
    public override async Task<ResultDTO<bool>> Handle(BuyProductCommand request, CancellationToken cancellationToken)
    {
        // check product availability
        var productQuery = productsRepository.Get(p => p.Id == request.ProductId);

        var productDTO = await productQuery.ProjectToType<ProductDTO>()
            .SingleOrDefaultAsync(cancellationToken);

        if (productDTO is null)
            return ResultDTO<bool>.Failure(statusCode: HttpStatusCode.NotFound,
                data: false,
                message: "Product not found");

        // check product quantity
        if (productDTO.Quantity < request.Quantity)
            return ResultDTO<bool>.Failure(statusCode: HttpStatusCode.BadRequest,
                data: false,
                message: "Product quantity is not enough");

        // update product quantity
        productDTO.Quantity -= request.Quantity;

        // mark the entity as modified
        await productsRepository.UpdateAsync(
            productQuery,
            updates => updates
                .SetProperty(p => p.Quantity, p => p.Quantity - request.Quantity),
            cancellationToken);

        return ResultDTO<bool>.Success(data: true);
    }
}
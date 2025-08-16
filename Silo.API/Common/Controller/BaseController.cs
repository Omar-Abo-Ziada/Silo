namespace Silo.API.Common.Controller;

[ApiController]
[Route("api/")]
public class BaseController<TRequest, TResponse>(BaseControllerParams<TRequest> baseControllerParams) : ControllerBase
{
    protected readonly IMediator _mediator = baseControllerParams.Mediator;
    protected readonly IMapperHelper _mapper = baseControllerParams.Mapper;
    protected readonly IValidator<TRequest> _validator = baseControllerParams.Validator ?? throw new ArgumentNullException(nameof(baseControllerParams.Validator));

    protected virtual async Task<ApiResponse<TResponse>> ValidateRequestAsync(TRequest request)
    {
        if (_validator is null)
            return ApiResponse<TResponse>.Success();

        var validationResults = await _validator.ValidateAsync(request);

        if (validationResults.IsValid)
            return ApiResponse<TResponse>.Success();


        var validationErrors = string.Join(", ", validationResults.Errors.Select(e => e.ErrorMessage));
        var errMsg = string.Format("Validation failed:\n {0}", validationErrors);
        var error = new Error(HttpStatusCode.BadRequest, errMsg, ErrorType.Validation);
        return ApiResponse<TResponse>.Failure(errors: [error]);
    }

    protected ApiResponse<TDist> HandleResult<TSource, TDist>(ResultDTO<TSource> result)
    {
        if (result is { IsSuccess: true, Data: null })
            return ApiResponse<TDist>.Success();
        return result.IsSuccess ? ApiResponse<TDist>.Success(data: result.Data.Adapt<TDist>()):
            ApiResponse<TDist>.Failure(errors: result.Errors);
    }
}
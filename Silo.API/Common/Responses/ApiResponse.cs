namespace Silo.API.Common.Responses;

public class ApiResponse<TResponse>
{
    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; }
    public IEnumerable<Error>? Errors { get; set; }
    public TResponse? Data { get; }
    public TokenDTO? TokenDTO { get; set; }

    private ApiResponse(bool isSuccess, HttpStatusCode statusCode, string? message = null,
        TResponse? data = default, IEnumerable<Error>? errors = null, TokenDTO? tokenDTO = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
        TokenDTO = tokenDTO;
    }

    public static ApiResponse<TResponse> Success(string? message = "Operation successful.", TResponse? data = default,
        HttpStatusCode statusCode = HttpStatusCode.OK, TokenDTO? tokenResult = null)
     => new(isSuccess: true, statusCode: statusCode, message: message, data: data, tokenDTO: tokenResult);

    public static ApiResponse<TResponse> Failure(string? message = "Operation failed.", TResponse? data = default,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<Error>? errors = null)
        => new(isSuccess: false, statusCode: statusCode, message: message, data: data, errors: errors);
}

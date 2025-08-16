namespace Silo.API.Common.Responses;

public class ResultDTO<TSource>
{
    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; set; }
    public string? Message { get; }
    public IEnumerable<Error>? Errors { get; set; }
    public TSource? Data { get; }
    public TokenDTO? TokenDTO { get; set; }

    private ResultDTO(bool isSuccess, HttpStatusCode statusCode, string? message = null,
        TSource? data = default, IEnumerable<Error>? errors = null, TokenDTO? tokenDTO = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
        Data = data;
        Errors = errors;
        TokenDTO = tokenDTO;
    }

    public static ResultDTO<TSource> Success(string? message = "Operation successful.", TSource? data = default,
        HttpStatusCode statusCode = HttpStatusCode.OK, TokenDTO? tokenResult = null)
     => new(isSuccess: true, statusCode: statusCode, message: message, data: data, tokenDTO: tokenResult);

    public static ResultDTO<TSource> Failure(string? message = "Operation failed.", TSource? data = default,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest, IEnumerable<Error>? errors = null)
        => new(isSuccess: false, statusCode: statusCode, message: message, data: data, errors: errors);
}

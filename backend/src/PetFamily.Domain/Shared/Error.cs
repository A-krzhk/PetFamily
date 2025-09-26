using PetFamily.Domain.Enums;

namespace PetFamily.Domain.Shared;

public record Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        Type = errorType;
    }
    
    public static Error Validation(string code, string message) =>
        new Error(code, message, ErrorType.Validation);
    
    public static Error NotFound(string code, string message) =>
        new Error(code, message, ErrorType.NotFound);

    public static Error BadRequest(string code, string message) =>
        new Error(code, message, ErrorType.BadRequest);

    public static Error InternalServerError(string code, string message) =>
        new Error(code, message, ErrorType.InternalServerError);

    public static Error Unauthorized(string code, string message) =>
        new Error(code, message, ErrorType.Unauthorized);

    public static Error Forbidden(string code, string message) =>
        new Error(code, message, ErrorType.Forbidden);

    public static Error Conflict(string code, string message) =>
        new Error(code, message, ErrorType.Conflict);
};
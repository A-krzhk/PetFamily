namespace PetFamily.Domain.Enums;

public enum ErrorType
{
    Validation,
    NotFound,
    BadRequest,
    InternalServerError,
    Unauthorized,
    Forbidden,
    Conflict
}
using Microsoft.AspNetCore.Mvc;
using PetFamily.Contracts.Response;
using PetFamily.Domain.Enums;
using PetFamily.Domain.Shared;


namespace PetFamily.API.Extensions;

public static class ResponseExtensions
{
    public static ActionResult ToResponse(this Error error)
    {
        var errorType = error.Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        
        var envelope = Envelope.Error(error);
        
        return new ObjectResult(envelope)
        {
            StatusCode = errorType
        };
    }
    
}
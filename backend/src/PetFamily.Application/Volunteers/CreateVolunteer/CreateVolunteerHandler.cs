using CSharpFunctionalExtensions;
using PetFamily.Contracts.Requests;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
{
    public async Task<Result<Guid, Error>> HandleAsync(CreateVolunteerRequest request, CancellationToken ct = default)
    {
        // Checking if such a record exists
        var volunteerByEmail = volunteerRepository.GetByEmail(request.Email, ct);
        if (volunteerByEmail.Result.IsSuccess)
            return Errors.General.ValueAlreadyExist(request.Email);
        
        var volunteerByPhone = volunteerRepository.GetByPhoneNumber(
            request.PhoneNumber.CountryCode,
            request.PhoneNumber.Number,
            ct);
        if (volunteerByPhone.Result.IsSuccess)
            return Errors.General.ValueAlreadyExist($"{request.PhoneNumber.CountryCode}{request.PhoneNumber.Number}");
        
        //Creating
        var fullNameResult = FullName.Create(
            request.FullName.FirstName, 
            request.FullName.LastName, 
            request.FullName.MiddleName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;
        
        var emailResult = Email.Create(request.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;

        var phoneNumberResult = PhoneNumber.Create(
            request.PhoneNumber.CountryCode,
            request.PhoneNumber.Number);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;
        
        var volunteerResult = Volunteer.Create(
            fullNameResult.Value,
            emailResult.Value,
            request.Description,
            request.WorkExperience,
            phoneNumberResult.Value
            );
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        //Saving
        await volunteerRepository.Add(volunteerResult.Value, ct);
        
        return volunteerResult.Value.Id;
    }
}
using CSharpFunctionalExtensions;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Volunteers.CreateVolunteer;

public class CreateVolunteerHandler(IVolunteerRepository volunteerRepository)
{
    public async Task<Result<Guid, Error>> 
        HandleAsync(CreateVolunteerCommand command, CancellationToken ct = default)
    {
        // Checking if such a record exists
        var volunteerByEmail = volunteerRepository.GetByEmail(command.CreateVolunteerRequest.Email, ct);
        if (volunteerByEmail.Result.IsSuccess)
            return Errors.General.ValueAlreadyExist(command.CreateVolunteerRequest.Email);
        
        var volunteerByPhone = volunteerRepository.GetByPhoneNumber(
            command.CreateVolunteerRequest.PhoneNumber.CountryCode,
            command.CreateVolunteerRequest.PhoneNumber.Number,
            ct);
        if (volunteerByPhone.Result.IsSuccess)
            return Errors.General.ValueAlreadyExist($"{command.CreateVolunteerRequest.PhoneNumber.CountryCode}{command.CreateVolunteerRequest.PhoneNumber.Number}");
        
        //Creating
        var fullNameResult = FullName.Create(
            command.CreateVolunteerRequest.FullName.FirstName, 
            command.CreateVolunteerRequest.FullName.LastName, 
            command.CreateVolunteerRequest.FullName.MiddleName);
        if (fullNameResult.IsFailure)
            return fullNameResult.Error;
        
        var emailResult = Email.Create(command.CreateVolunteerRequest.Email);
        if (emailResult.IsFailure)
            return emailResult.Error;

        var phoneNumberResult = PhoneNumber.Create(
            command.CreateVolunteerRequest.PhoneNumber.CountryCode,
            command.CreateVolunteerRequest.PhoneNumber.Number);
        if (phoneNumberResult.IsFailure)
            return phoneNumberResult.Error;
        
        var volunteerResult = Volunteer.Create(
            fullNameResult.Value,
            emailResult.Value,
            command.CreateVolunteerRequest.Description,
            command.CreateVolunteerRequest.WorkExperience,
            phoneNumberResult.Value
            );
        if (volunteerResult.IsFailure)
            return volunteerResult.Error;
        
        var volunteer = volunteerResult.Value;
        
        foreach (var requisiteRequestDto in command.CreateVolunteerRequest.HelpRequisites)
        {
            var requisiteResult = HelpRequisites.Create(
                requisiteRequestDto.Recipient,
                requisiteRequestDto.TIN,
                requisiteRequestDto.TRRC,
                requisiteRequestDto.RCRN,
                requisiteRequestDto.RCBIC,
                requisiteRequestDto.Acc,
                requisiteRequestDto.CorrAcc);
            if (requisiteResult.IsFailure)
                return requisiteResult.Error;

            volunteer.AddHelpRequisite(requisiteResult.Value);
        }
        
        foreach (var socialMediaDto in command.CreateVolunteerRequest.SocialMedias)
        {
            var socialMediaResult = SocialMedia.Create(socialMediaDto.Name, socialMediaDto.Url);
            if (socialMediaResult.IsFailure)
                return socialMediaResult.Error;

            volunteer.AddSocialMedia(socialMediaResult.Value);
        }
        
        //Saving
        await volunteerRepository.Add(volunteerResult.Value, ct);
        
        return volunteerResult.Value.Id;
    }
}
using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.ValueObjects;

public record HelpRequisites
{
    //ef core
    private HelpRequisites() { }
    
    private HelpRequisites(
        string recipient, 
        string tin, 
        string trrc,
        string rcrn,
        string rcbic,
        string acc,
        string corAcc)
    {
        Recipient = recipient;
        TIN = tin;
        TRRC = trrc;
        RCRN = rcrn;
        RCBIC = rcbic;
        Acc = acc;
        CorrAcc = corAcc;
    }
    
    public string Recipient { get; } // Получатель
    public string TIN { get; } // ИНН
    public string TRRC { get; } // КПП
    public string RCRN { get; } // ОГРН
    public string RCBIC { get; } // БИК
    public string Acc { get; } // Р/с 
    public string CorrAcc { get; } // К/с

    public static Result<HelpRequisites, Error> Create(
        string recipient, 
        string tin, 
        string trrc,
        string rcrn,
        string rcbic,
        string acc,
        string corAcc)
    {
        if (string.IsNullOrWhiteSpace(recipient))
            return Errors.General.ValueIsRequired(nameof(recipient));
        if (string.IsNullOrWhiteSpace(tin))
            return Errors.General.ValueIsRequired(nameof(tin));
        if (string.IsNullOrWhiteSpace(trrc))
            return Errors.General.ValueIsRequired(nameof(trrc));
        if (string.IsNullOrWhiteSpace(rcrn))
            return Errors.General.ValueIsRequired(nameof(rcrn));
        if (string.IsNullOrWhiteSpace(rcbic))
            return Errors.General.ValueIsRequired(nameof(rcbic));
        if (string.IsNullOrWhiteSpace(acc))
            return Errors.General.ValueIsRequired(nameof(acc));
        if (string.IsNullOrWhiteSpace(corAcc))
            return Errors.General.ValueIsRequired(nameof(corAcc));
        return new HelpRequisites(recipient, tin, trrc, rcrn, rcbic, acc, corAcc);
    }
}
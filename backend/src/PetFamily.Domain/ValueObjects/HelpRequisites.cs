using CSharpFunctionalExtensions;

namespace PetFamily.Domain.ValueObjects;

public record HelpRequisites
{
    private HelpRequisites(
        string recipient, 
        string tin, 
        string trrc,
        string rcrn,
        string rcbic,
        string acc,
        string corAcc,
        string bank)
    {
        Recipient = recipient;
        TIN = tin;
        TRRC = trrc;
        RCRN = rcrn;
        RCBIC = rcbic;
        Acc = acc;
        CorrAcc = corAcc;
        Bank = bank;
    }
    
    public string Recipient { get; } // Получатель
    public string TIN { get; } // ИНН
    public string TRRC { get; } // КПП
    public string RCRN { get; } // ОГРН
    public string RCBIC { get; } // БИК
    public string Acc { get; } // Р/с 
    public string CorrAcc { get; } // К/с
    public string Bank { get; }

    public Result<HelpRequisites, string> Create(
        string recipient, 
        string tin, 
        string trrc,
        string rcrn,
        string rcbic,
        string acc,
        string corAcc,
        string bank)
    {
        if (string.IsNullOrWhiteSpace(recipient))
            return "Recipient can not be null";
        if (string.IsNullOrWhiteSpace(tin))
            return "TIN can not be null";
        if (string.IsNullOrWhiteSpace(trrc))
            return "TRRC can not be null";
        if (string.IsNullOrWhiteSpace(rcrn))
            return "RCRN can not be null";
        if (string.IsNullOrWhiteSpace(rcbic))
            return "RCBIC can not be null";
        if (string.IsNullOrWhiteSpace(acc))
            return "Acc can not be null";
        if (string.IsNullOrWhiteSpace(corAcc))
            return "CorAcc can not be null";
        if (string.IsNullOrWhiteSpace(bank))
            return "Bank can not be null";
        return new HelpRequisites( recipient, tin, trrc, rcrn, rcbic, acc, corAcc, bank);
    }
}
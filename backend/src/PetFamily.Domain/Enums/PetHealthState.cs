namespace PetFamily.Domain.Enums;

public enum PetHealthState
{
    Healthy,                // Полностью здоров
    Sick,                   // Болен, требует лечения
    UnderTreatment,         // Сейчас проходит лечение
    PostSurgery,            // Прошел операцию, восстанавливается
    NeedsSurgery,           // Требуется операция
    Disabled,               // Животное с инвалидностью
    Unknown                 // Состояние не известно
}
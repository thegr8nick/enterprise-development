namespace Library.Application.Contracts.Readers;

/// <summary>
/// DTO для создания или обновления читателя
/// </summary>
public class ReaderCreateUpdateDto
{
    /// <summary>
    /// ФИО читателя
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Адрес читателя
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Телефон читателя
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Дата регистрации читателя
    /// </summary>
    public DateTime? RegistrationDate { get; set; }
}
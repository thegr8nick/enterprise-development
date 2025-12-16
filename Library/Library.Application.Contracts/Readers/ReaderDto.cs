namespace Library.Application.Contracts.Readers;

/// <summary>
/// DTO для получения читателя
/// </summary>
public class ReaderDto
{
    /// <summary>
    /// Уникальный идентификатор читателя
    /// </summary>
    public required int Id { get; set; }

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
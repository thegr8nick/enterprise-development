namespace Library.Domain.Models;

/// <summary>
/// Сущность читателя библиотеки с персональными данными и историей выдач
/// </summary>
public class Reader
{
    /// <summary>
    /// Уникальный идентификатор
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

    /// <summary>
    /// Выданные читателю книги
    /// </summary>
    public ICollection<BookIssue> BookIssues { get; set; } = [];
}
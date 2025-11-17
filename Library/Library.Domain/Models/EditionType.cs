namespace Library.Domain.Models;

/// <summary>
/// Справочник видов издания, используемый для классификации книг
/// </summary>
public class EditionType
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Наименование вида издания
    /// </summary>
    public required string Name { get; set; }
}
namespace Library.Domain.Models;

/// <summary>
/// Справочник издательств, к которым относятся книги
/// </summary>
public class Publisher
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Наименование издательства
    /// </summary>
    public required string Name { get; set; }
}
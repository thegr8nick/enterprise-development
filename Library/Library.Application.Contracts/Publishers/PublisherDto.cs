namespace Library.Application.Contracts.Publishers;

/// <summary>
/// DTO для получения издательства
/// </summary>
public class PublisherDto
{
    /// <summary>
    /// Уникальный идентификатор издательства
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Наименование издательства
    /// </summary>
    public required string Name { get; set; }
}
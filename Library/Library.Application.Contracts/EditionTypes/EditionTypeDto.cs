namespace Library.Application.Contracts.EditionTypes;

/// <summary>
/// DTO для получения вида издания
/// </summary>
public class EditionTypeDto
{
    /// <summary>
    /// Уникальный идентификатор вида издания
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Наименование вида издания
    /// </summary>
    public required string Name { get; set; }
}
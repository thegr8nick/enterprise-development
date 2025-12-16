namespace Library.Application.Contracts.EditionTypes;

/// <summary>
/// DTO для создания или обновления вида издания
/// </summary>
public class EditionTypeCreateUpdateDto
{
    /// <summary>
    /// Наименование вида издания
    /// </summary>
    public required string Name { get; set; }
}
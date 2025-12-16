namespace Library.Application.Contracts.Publishers;

/// <summary>
/// DTO для создания или обновления издательства
/// </summary>
public class PublisherCreateUpdateDto
{
    /// <summary>
    /// Наименование издательства
    /// </summary>
    public required string Name { get; set; }
}
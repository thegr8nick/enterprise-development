namespace Library.Application.Contracts.Books;

/// <summary>
/// DTO для создания или обновления книги
/// </summary>
public class BookCreateUpdateDto
{
    /// <summary>
    /// Инвентарный номер
    /// </summary>
    public required string InventoryNumber { get; set; }

    /// <summary>
    /// Шифр в алфавитном каталоге
    /// </summary>
    public required string AlphabetCode { get; set; }

    /// <summary>
    /// Инициалы и фамилии авторов
    /// </summary>
    public string? Authors { get; set; }

    /// <summary>
    /// Название книги
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Идентификатор вида издания
    /// </summary>
    public required int EditionTypeId { get; set; }

    /// <summary>
    /// Идентификатор издательства
    /// </summary>
    public required int PublisherId { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    public int Year { get; set; }
}
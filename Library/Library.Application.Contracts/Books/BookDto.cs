namespace Library.Application.Contracts.Books;

/// <summary>
/// DTO для получения книги
/// </summary>
public class BookDto
{
    /// <summary>
    /// Уникальный идентификатор книги
    /// </summary>
    public required int Id { get; set; }

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
    /// Год издания
    /// </summary>
    public int Year { get; set; }
}
namespace Library.Domain.Models;

/// <summary>
/// Сущность книги, содержащая сведения из каталога библиотеки
/// </summary>
public class Book
{
    /// <summary>
    /// Уникальный идентификатор
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
    /// Название
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Идентификатор вида издания
    /// </summary>
    public required int EditionTypeId { get; set; }

    /// <summary>
    /// Вид издания
    /// </summary>
    public EditionType? EditionType { get; set; }

    /// <summary>
    /// Идентификатор издательства
    /// </summary>
    public required int PublisherId { get; set; }

    /// <summary>
    /// Издательство
    /// </summary>
    public Publisher? Publisher { get; set; }

    /// <summary>
    /// Год издания
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Записи о выдаче книги
    /// </summary>
    public ICollection<BookIssue> Issues { get; set; } = [];
}
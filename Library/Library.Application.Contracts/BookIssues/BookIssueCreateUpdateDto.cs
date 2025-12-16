namespace Library.Application.Contracts.BookIssues;

/// <summary>
/// DTO для создания или обновления факта выдачи книги
/// </summary>
public class BookIssueCreateUpdateDto
{
    /// <summary>
    /// Идентификатор книги
    /// </summary>
    public required int BookId { get; set; }

    /// <summary>
    /// Идентификатор читателя
    /// </summary>
    public required int ReaderId { get; set; }

    /// <summary>
    /// Дата выдачи книги
    /// </summary>
    public required DateTime IssueDate { get; set; }

    /// <summary>
    /// Количество дней, на которое выдана книга
    /// </summary>
    public required int Days { get; set; }

    /// <summary>
    /// Дата возврата книги если null то книга не возвращена
    /// </summary>
    public DateTime? ReturnDate { get; set; }
}
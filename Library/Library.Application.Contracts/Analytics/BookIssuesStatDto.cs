using Library.Application.Contracts.Books;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для получения статистики по книге
/// </summary>
public class BookIssuesStatDto
{
    /// <summary>
    /// DTO для получения книги
    /// </summary>
    public required BookDto Book { get; set; }

    /// <summary>
    /// Количество выдач
    /// </summary>
    public required int IssuesCount { get; set; }
}
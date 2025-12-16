using Library.Application.Contracts.Publishers;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для получения статистики по издательству
/// </summary>
public class PublisherIssuesStatDto
{
    /// <summary>
    /// DTO для получения издательства
    /// </summary>
    public required PublisherDto Publisher { get; set; }

    /// <summary>
    /// Количество выдач
    /// </summary>
    public required int IssuesCount { get; set; }
}
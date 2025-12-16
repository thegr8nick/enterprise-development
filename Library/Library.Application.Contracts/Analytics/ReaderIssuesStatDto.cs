using Library.Application.Contracts.Readers;

namespace Library.Application.Contracts.Analytics;

/// <summary>
/// DTO для получения статистики по читателю
/// </summary>
public class ReaderIssuesStatDto
{
    /// <summary>
    /// DTO для получения читателя
    /// </summary>
    public required ReaderDto Reader { get; set; }

    /// <summary>
    /// Количество выдач
    /// </summary>
    public required int IssuesCount { get; set; }
}
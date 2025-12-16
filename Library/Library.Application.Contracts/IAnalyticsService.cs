using Library.Application.Contracts.Analytics;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.Readers;

namespace Library.Application.Contracts;

/// <summary>
/// Сервис аналитических запросов по доменной области библиотеки
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает информацию о выданных книгах, упорядоченных по названию
    /// </summary>
    public Task<IList<BookDto>> GetIssuedBooksOrderedByTitle();

    /// <summary>
    /// Возвращает топ 5 читателей, прочитавших больше всего книг за заданный период
    /// </summary>
    /// <param name="periodStart">Начало периода в UTC</param>
    /// <param name="periodEnd">Конец периода в UTC</param>
    public Task<IList<ReaderIssuesStatDto>> GetTop5ReadersByIssuesCount(DateTime periodStart, DateTime periodEnd);

    /// <summary>
    /// Возвращает читателей, бравших книги на наибольший период времени, упорядоченных по ФИО
    /// </summary>
    public Task<IList<ReaderDto>> GetReadersByMaxLoanDaysOrderedByFullName();

    /// <summary>
    /// Возвращает топ 5 наиболее популярных издательств за последний год
    /// </summary>
    /// <param name="nowUtc">Текущая точка времени в UTC для расчёта периода</param>
    public Task<IList<PublisherIssuesStatDto>> GetTop5PublishersByIssuesCountLastYear(DateTime nowUtc);

    /// <summary>
    /// Возвращает топ 5 наименее популярных книг за последний год
    /// </summary>
    /// <param name="nowUtc">Текущая точка времени в UTC для расчёта периода</param>
    public Task<IList<BookIssuesStatDto>> GetBottom5BooksByIssuesCountLastYear(DateTime nowUtc);
}
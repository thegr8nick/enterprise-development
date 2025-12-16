using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.Analytics;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.Publishers;
using Library.Application.Contracts.Readers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;


/// <summary>
/// Сервис аналитических запросов по доменной области библиотеки
/// </summary>
public class AnalyticsService(
    IRepository<BookIssue, int> bookIssueRepository,
    IRepository<Book, int> bookRepository,
    IRepository<Reader, int> readerRepository,
    IRepository<Publisher, int> publisherRepository,
    IMapper mapper) : IAnalyticsService
{
    /// <summary>
    /// Возвращает информацию о выданных книгах, упорядоченных по названию
    /// </summary>
    public async Task<IList<BookDto>> GetIssuedBooksOrderedByTitle()
    {
        var issues = await bookIssueRepository.ReadAll();
        var books = await bookRepository.ReadAll();

        var issuedBooks = issues
            .Where(bi => bi.ReturnDate == null)
            .Join(books,
                bi => bi.BookId,
                b => b.Id,
                (bi, b) => b)
            .OrderBy(b => b.Title)
            .ToList();

        return mapper.Map<IList<BookDto>>(issuedBooks);
    }

    /// <summary>
    /// Возвращает топ 5 читателей, прочитавших больше всего книг за заданный период
    /// </summary>
    public async Task<IList<ReaderIssuesStatDto>> GetTop5ReadersByIssuesCount(DateTime periodStart, DateTime periodEnd)
    {
        var issues = await bookIssueRepository.ReadAll();
        var readers = await readerRepository.ReadAll();

        var topReaders = issues
            .Where(bi => bi.IssueDate >= periodStart && bi.IssueDate <= periodEnd)
            .GroupBy(bi => bi.ReaderId)
            .Select(g => new { ReaderId = g.Key, Count = g.Count() })
            .Join(readers, g => g.ReaderId, r => r.Id, (g, r) => new { Reader = r, g.Count })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Reader.FullName)
            .Take(5)
            .Select(x => new ReaderIssuesStatDto
            {
                Reader = mapper.Map<ReaderDto>(x.Reader),
                IssuesCount = x.Count
            })
            .ToList();

        return topReaders;
    }

    /// <summary>
    /// Возвращает информацию о читателях, бравших книги на наибольший период времени, упорядоченную по ФИО
    /// </summary>
    public async Task<IList<ReaderDto>> GetReadersByMaxLoanDaysOrderedByFullName()
    {
        var issues = await bookIssueRepository.ReadAll();
        var readers = await readerRepository.ReadAll();

        var maxDays = issues.Max(bi => bi.Days);

        var resultReaders = issues
            .Where(bi => bi.Days == maxDays)
            .Select(bi => bi.ReaderId)
            .Distinct()
            .Join(readers, id => id, r => r.Id, (id, r) => r)
            .OrderBy(r => r.FullName)
            .ToList();

        return mapper.Map<IList<ReaderDto>>(resultReaders);
    }

    /// <summary>
    /// Возвращает топ 5 наиболее популярных издательств за последний год
    /// </summary>
    public async Task<IList<PublisherIssuesStatDto>> GetTop5PublishersByIssuesCountLastYear(DateTime nowUtc)
    {
        var issues = await bookIssueRepository.ReadAll();
        var books = await bookRepository.ReadAll();
        var publishers = await publisherRepository.ReadAll();

        var lastYearStart = nowUtc.AddYears(-1);
        var lastYearEnd = nowUtc;

        var result = issues
            .Where(bi => bi.IssueDate >= lastYearStart && bi.IssueDate <= lastYearEnd)
            .Join(books, bi => bi.BookId, b => b.Id, (bi, b) => b.PublisherId)
            .GroupBy(pid => pid)
            .Select(g => new { PublisherId = g.Key, Count = g.Count() })
            .Join(publishers, g => g.PublisherId, p => p.Id, (g, p) => new { Publisher = p, g.Count })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Publisher.Name)
            .Take(5)
            .Select(x => new PublisherIssuesStatDto
            {
                Publisher = mapper.Map<PublisherDto>(x.Publisher),
                IssuesCount = x.Count
            })
            .ToList();

        return result;
    }

    /// <summary>
    /// Возвращает топ 5 наименее популярных книг за последний год
    /// </summary>
    public async Task<IList<BookIssuesStatDto>> GetBottom5BooksByIssuesCountLastYear(DateTime nowUtc)
    {
        var issues = await bookIssueRepository.ReadAll();
        var books = await bookRepository.ReadAll();

        var lastYearStart = nowUtc.AddYears(-1);
        var lastYearEnd = nowUtc;

        var issuesInPeriod = issues
            .Where(bi => bi.IssueDate >= lastYearStart && bi.IssueDate <= lastYearEnd);

        var result = books
            .GroupJoin(
                issuesInPeriod,
                b => b.Id,
                bi => bi.BookId,
                (b, joinedIssues) => new BookIssuesStatDto
                {
                    Book = mapper.Map<BookDto>(b),
                    IssuesCount = joinedIssues.Count()
                })
            .OrderBy(x => x.IssuesCount)
            .ThenBy(x => x.Book.Title)
            .Take(5)
            .ToList();

        return result;
    }
}
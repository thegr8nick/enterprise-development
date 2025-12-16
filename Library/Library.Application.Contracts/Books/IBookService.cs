using Library.Application.Contracts.BookIssues;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;

namespace Library.Application.Contracts.Books;

/// <summary>
/// Сервис приложения для работы с книгами
/// </summary>
public interface IBookService : IApplicationService<BookDto, BookCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает записи о выдачах книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>Список DTO для получения выдач книг</returns>
    public Task<IList<BookIssueDto>> GetIssues(int bookId);

    /// <summary>
    /// Возвращает вид издания книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>DTO для получения вида издания</returns>
    public Task<EditionTypeDto> GetEditionType(int bookId);

    /// <summary>
    /// Возвращает издательство книги
    /// </summary>
    /// <param name="bookId">Идентификатор книги</param>
    /// <returns>DTO для получения издательства</returns>
    public Task<PublisherDto> GetPublisher(int bookId);
}
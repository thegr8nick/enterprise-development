using Library.Application.Contracts.BookIssues;

namespace Library.Application.Contracts.Readers;

/// <summary>
/// Сервис приложения для работы с читателями
/// </summary>
public interface IReaderService : IApplicationService<ReaderDto, ReaderCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает записи о выдачах книг читателю
    /// </summary>
    /// <param name="readerId">Идентификатор читателя</param>
    /// <returns>Список DTO для получения выдач книг</returns>
    public Task<IList<BookIssueDto>> GetIssues(int readerId);
}
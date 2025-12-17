using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.BookIssues;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис приложения для работы с выдачами книг
/// </summary>
public class BookIssueService(
    IRepository<BookIssue, int> bookIssueRepository,
    IRepository<Book, int> bookRepository,
    IRepository<Reader, int> readerRepository,
    IMapper mapper) : IApplicationService<BookIssueDto, BookIssueCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт выдачу книги
    /// </summary>
    /// <param name="dto">DTO для создания или обновления выдачи книги</param>
    /// <returns>DTO для получения выдачи книги</returns>
    public async Task<BookIssueDto> Create(BookIssueCreateUpdateDto dto)
    {
        _ = await bookRepository.Read(dto.BookId)
            ?? throw new KeyNotFoundException($"Book with id {dto.BookId} not found");

        _ = await readerRepository.Read(dto.ReaderId)
            ?? throw new KeyNotFoundException($"Reader with id {dto.ReaderId} not found");

        var entity = mapper.Map<BookIssue>(dto);

        var created = await bookIssueRepository.Create(entity);
        return mapper.Map<BookIssueDto>(created);
    }

    /// <summary>
    /// Возвращает выдачу книги по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор выдачи книги</param>
    /// <returns>DTO для получения выдачи книги</returns>
    public async Task<BookIssueDto?> Get(int dtoId)
    {
        var entity = await bookIssueRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Book Issue with id {dtoId} not found");

        return mapper.Map<BookIssueDto>(entity);
    }

    /// <summary>
    /// Возвращает список выдач книг
    /// </summary>
    /// <returns>Список DTO для получения выдач книг</returns>
    public async Task<IList<BookIssueDto>> GetAll()
    {
        var items = await bookIssueRepository.ReadAll();
        return [.. items.Select(mapper.Map<BookIssueDto>)];
    }

    /// <summary>
    /// Обновляет выдачу книги по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания или обновления выдачи книги</param>
    /// <param name="dtoId">Идентификатор выдачи книги</param>
    /// <returns>DTO для получения выдачи книги</returns>
    public async Task<BookIssueDto> Update(BookIssueCreateUpdateDto dto, int dtoId)
    {
        _ = await bookRepository.Read(dto.BookId)
            ?? throw new KeyNotFoundException($"Book with id {dto.BookId} not found");

        _ = await readerRepository.Read(dto.ReaderId)
            ?? throw new KeyNotFoundException($"Reader with id  {dto.ReaderId}  not found");

        var entity = await bookIssueRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Book Issue with id {dtoId} not found");

        mapper.Map(dto, entity);

        var updated = await bookIssueRepository.Update(entity);
        return mapper.Map<BookIssueDto>(updated);
    }

    /// <summary>
    /// Удаляет выдачу книги по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор выдачи книги</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => bookIssueRepository.Delete(dtoId);
}
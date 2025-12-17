using AutoMapper;
using Library.Application.Contracts.BookIssues;
using Library.Application.Contracts.Readers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис приложения для работы с читателями
/// </summary>
public class ReaderService(
    IRepository<Reader, int> readerRepository,
    IRepository<BookIssue, int> bookIssueRepository,
    IMapper mapper) : IReaderService
{
    /// <summary>
    /// Создаёт читателя
    /// </summary>
    /// <param name="dto">DTO для создания или обновления читателя</param>
    /// <returns>DTO для получения читателя</returns>
    public async Task<ReaderDto> Create(ReaderCreateUpdateDto dto)
    {
        var entity = mapper.Map<Reader>(dto);

        var created = await readerRepository.Create(entity);
        return mapper.Map<ReaderDto>(created);
    }

    /// <summary>
    /// Возвращает читателя по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <returns>DTO для получения читателя</returns>
    public async Task<ReaderDto?> Get(int dtoId)
    {
        var entity = await readerRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Reader with id {dtoId} not found");

        return mapper.Map<ReaderDto>(entity);
    }

    /// <summary>
    /// Возвращает список читателей
    /// </summary>
    /// <returns>Список DTO для получения читателей</returns>
    public async Task<IList<ReaderDto>> GetAll()
    {
        var items = await readerRepository.ReadAll();
        return [.. items.Select(mapper.Map<ReaderDto>)];
    }

    /// <summary>
    /// Обновляет читателя по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания или обновления читателя</param>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <returns>DTO для получения читателя</returns>
    public async Task<ReaderDto> Update(ReaderCreateUpdateDto dto, int dtoId)
    {
        var entity = await readerRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Reader with id {dtoId} not found");

        mapper.Map(dto, entity);

        var updated = await readerRepository.Update(entity);
        return mapper.Map<ReaderDto>(updated);
    }

    /// <summary>
    /// Удаляет читателя по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор читателя</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => readerRepository.Delete(dtoId);

    /// <summary>
    /// Возвращает записи о выдачах книг читателю
    /// </summary>
    /// <param name="readerId">Идентификатор читателя</param>
    /// <returns>Список DTO для получения выдач книг</returns>
    public async Task<IList<BookIssueDto>> GetIssues(int readerId)
    {
        _ = await readerRepository.Read(readerId)
            ?? throw new KeyNotFoundException($"Reader with id {readerId} not found");

        var issues = await bookIssueRepository.ReadAll();

        var readerIssues = issues
            .Where(x => x.ReaderId == readerId)
            .ToList();

        return [.. readerIssues.Select(mapper.Map<BookIssueDto>)];
    }
}
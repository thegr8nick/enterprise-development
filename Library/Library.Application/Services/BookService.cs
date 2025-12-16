using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.Books;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис приложения для работы с книгами
/// </summary>
public class BookService(
    IRepository<Book, int> bookRepository,
    IRepository<Publisher, int> publisherRepository,
    IRepository<EditionType, int> editionTypeRepository,
    IMapper mapper) : IApplicationService<BookDto, BookCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт книгу
    /// </summary>
    /// <param name="dto">DTO для создания или обновления книги</param>
    /// <returns>DTO для получения книги</returns>
    public async Task<BookDto> Create(BookCreateUpdateDto dto)
    {
        _ = await publisherRepository.Read(dto.PublisherId)
            ?? throw new KeyNotFoundException($"Издательство с идентификатором {dto.PublisherId} не найдено");

        _ = await editionTypeRepository.Read(dto.EditionTypeId)
            ?? throw new KeyNotFoundException($"Вид издания с идентификатором {dto.EditionTypeId} не найден");

        var entity = mapper.Map<Book>(dto);

        var created = await bookRepository.Create(entity);
        return mapper.Map<BookDto>(created);
    }

    /// <summary>
    /// Возвращает книгу по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <returns>DTO для получения книги</returns>
    public async Task<BookDto?> Get(int dtoId)
    {
        var entity = await bookRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Книга с идентификатором {dtoId} не найдена");

        return mapper.Map<BookDto>(entity);
    }

    /// <summary>
    /// Возвращает список книг
    /// </summary>
    /// <returns>Список DTO для получения книг</returns>
    public async Task<IList<BookDto>> GetAll()
    {
        var items = await bookRepository.ReadAll();
        return [.. items.Select(mapper.Map<BookDto>)];
    }

    /// <summary>
    /// Обновляет книгу по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания или обновления книги</param>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <returns>DTO для получения книги</returns>
    public async Task<BookDto> Update(BookCreateUpdateDto dto, int dtoId)
    {
        _ = await publisherRepository.Read(dto.PublisherId)
            ?? throw new KeyNotFoundException($"Издательство с идентификатором {dto.PublisherId} не найдено");

        _ = await editionTypeRepository.Read(dto.EditionTypeId)
            ?? throw new KeyNotFoundException($"Вид издания с идентификатором {dto.EditionTypeId} не найден");

        var entity = await bookRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Книга с идентификатором {dtoId} не найдена");

        mapper.Map(dto, entity);

        var updated = await bookRepository.Update(entity);
        return mapper.Map<BookDto>(updated);
    }

    /// <summary>
    /// Удаляет книгу по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор книги</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => bookRepository.Delete(dtoId);
}
using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.Publishers;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис приложения для работы с издательствами
/// </summary>
public class PublisherService(
    IRepository<Publisher, int> publisherRepository,
    IMapper mapper) : IApplicationService<PublisherDto, PublisherCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт издательство
    /// </summary>
    /// <param name="dto">DTO для создания или обновления издательства</param>
    /// <returns>DTO для получения издательства</returns>
    public async Task<PublisherDto> Create(PublisherCreateUpdateDto dto)
    {
        var entity = mapper.Map<Publisher>(dto);

        var created = await publisherRepository.Create(entity);
        return mapper.Map<PublisherDto>(created);
    }

    /// <summary>
    /// Возвращает издательство по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <returns>DTO для получения издательства</returns>
    public async Task<PublisherDto?> Get(int dtoId)
    {
        var entity = await publisherRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Издательство с идентификатором {dtoId} не найдено");

        return mapper.Map<PublisherDto>(entity);
    }

    /// <summary>
    /// Возвращает список издательств
    /// </summary>
    /// <returns>Список DTO для получения издательств</returns>
    public async Task<IList<PublisherDto>> GetAll()
    {
        var items = await publisherRepository.ReadAll();
        return [.. items.Select(mapper.Map<PublisherDto>)];
    }

    /// <summary>
    /// Обновляет издательство по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания или обновления издательства</param>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <returns>DTO для получения издательства</returns>
    public async Task<PublisherDto> Update(PublisherCreateUpdateDto dto, int dtoId)
    {
        var entity = await publisherRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Издательство с идентификатором {dtoId} не найдено");

        mapper.Map(dto, entity);

        var updated = await publisherRepository.Update(entity);
        return mapper.Map<PublisherDto>(updated);
    }

    /// <summary>
    /// Удаляет издательство по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор издательства</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => publisherRepository.Delete(dtoId);
}
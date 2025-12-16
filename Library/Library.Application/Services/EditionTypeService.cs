using AutoMapper;
using Library.Application.Contracts;
using Library.Application.Contracts.EditionTypes;
using Library.Domain;
using Library.Domain.Models;

namespace Library.Application.Services;

/// <summary>
/// Сервис приложения для работы с видами изданий
/// </summary>
public class EditionTypeService(
    IRepository<EditionType, int> editionTypeRepository,
    IMapper mapper) : IApplicationService<EditionTypeDto, EditionTypeCreateUpdateDto, int>
{
    /// <summary>
    /// Создаёт вид издания
    /// </summary>
    /// <param name="dto">DTO для создания или обновления вида издания</param>
    /// <returns>DTO для получения вида издания</returns>
    public async Task<EditionTypeDto> Create(EditionTypeCreateUpdateDto dto)
    {
        var entity = mapper.Map<EditionType>(dto);

        var created = await editionTypeRepository.Create(entity);
        return mapper.Map<EditionTypeDto>(created);
    }

    /// <summary>
    /// Возвращает вид издания по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор вида издания</param>
    /// <returns>DTO для получения вида издания</returns>
    public async Task<EditionTypeDto?> Get(int dtoId)
    {
        var entity = await editionTypeRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Вид издания с идентификатором {dtoId} не найден");

        return mapper.Map<EditionTypeDto>(entity);
    }

    /// <summary>
    /// Возвращает список видов изданий
    /// </summary>
    /// <returns>Список DTO для получения видов изданий</returns>
    public async Task<IList<EditionTypeDto>> GetAll()
    {
        var items = await editionTypeRepository.ReadAll();
        return [.. items.Select(mapper.Map<EditionTypeDto>)];
    }

    /// <summary>
    /// Обновляет вид издания по идентификатору
    /// </summary>
    /// <param name="dto">DTO для создания или обновления вида издания</param>
    /// <param name="dtoId">Идентификатор вида издания</param>
    /// <returns>DTO для получения вида издания</returns>
    public async Task<EditionTypeDto> Update(EditionTypeCreateUpdateDto dto, int dtoId)
    {
        var entity = await editionTypeRepository.Read(dtoId)
            ?? throw new KeyNotFoundException($"Вид издания с идентификатором {dtoId} не найден");

        mapper.Map(dto, entity);

        var updated = await editionTypeRepository.Update(entity);
        return mapper.Map<EditionTypeDto>(updated);
    }

    /// <summary>
    /// Удаляет вид издания по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор вида издания</param>
    /// <returns>true если удаление выполнено иначе false</returns>
    public Task<bool> Delete(int dtoId) => editionTypeRepository.Delete(dtoId);
}
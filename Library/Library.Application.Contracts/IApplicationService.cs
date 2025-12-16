namespace Library.Application.Contracts;

/// <summary>
/// Универсальный интерфейс службы приложения для CRUD операций над сущностями через DTO
/// </summary>
/// <typeparam name="TDto">DTO для операций чтения</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO для операций создания и обновления</typeparam>
/// <typeparam name="TKey">Тип идентификатора DTO</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto, TKey>
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт сущность на основе DTO для создания и возвращает DTO для чтения
    /// </summary>
    /// <param name="dto">DTO, содержащий данные для создания</param>
    /// <returns>Созданный объект в формате DTO для чтения</returns>
    public Task<TDto> Create(TCreateUpdateDto dto);

    /// <summary>
    /// Возвращает DTO для чтения по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор</param>
    /// <returns>DTO для чтения или null если объект не найден</returns>
    public Task<TDto?> Get(TKey dtoId);

    /// <summary>
    /// Возвращает список DTO для чтения
    /// </summary>
    /// <returns>Список DTO для чтения</returns>
    public Task<IList<TDto>> GetAll();

    /// <summary>
    /// Обновляет сущность по идентификатору на основе DTO для обновления и возвращает DTO для чтения
    /// </summary>
    /// <param name="dto">DTO, содержащий новые значения</param>
    /// <param name="dtoId">Идентификатор обновляемого объекта</param>
    /// <returns>Обновлённый объект в формате DTO для чтения</returns>
    public Task<TDto> Update(TCreateUpdateDto dto, TKey dtoId);

    /// <summary>
    /// Удаляет объект по идентификатору
    /// </summary>
    /// <param name="dtoId">Идентификатор удаляемого объекта</param>
    /// <returns>true если объект был удалён иначе false</returns>
    public Task<bool> Delete(TKey dtoId);
}
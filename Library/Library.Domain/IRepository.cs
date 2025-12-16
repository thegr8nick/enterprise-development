namespace Library.Domain;

/// <summary>
/// Обобщённый интерфейс репозитория, инкапсулирующий CRUD-операции над сущностью доменной модели
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности, над которой выполняются операции репозитория</typeparam>
/// <typeparam name="TKey">Тип первичного ключа (идентификатора) сущности</typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class
    where TKey : struct
{
    /// <summary>
    /// Создаёт новую сущность и сохраняет её в источнике данных
    /// </summary>
    /// <param name="entity">Экземпляр новой сущности для сохранения</param>
    /// <returns>Созданная сущность</returns>
    public Task<TEntity> Create(TEntity entity);

    /// <summary>
    /// Возвращает сущность по её идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор искомой сущности</param>
    /// <returns>
    /// Сущность, если она найдена; иначе null
    /// </returns>
    public Task<TEntity?> Read(TKey entityId);

    /// <summary>
    /// Возвращает полный список сущностей данного типа
    /// </summary>
    /// <returns>Список сущностей</returns>
    public Task<IList<TEntity>> ReadAll();

    /// <summary>
    /// Обновляет существующую сущность в источнике данных
    /// </summary>
    /// <param name="entity">Сущность с актуальными значениями полей</param>
    /// <returns>Обновлённая сущность</returns>
    public Task<TEntity> Update(TEntity entity);

    /// <summary>
    /// Удаляет сущность по её идентификатору
    /// </summary>
    /// <param name="entityId">Идентификатор удаляемой сущности</param>
    /// <returns>
    /// true, если сущность была найдена и удалена; иначе false
    /// </returns>
    public Task<bool> Delete(TKey entityId);
}
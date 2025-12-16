using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий видов изданий EditionType
/// </summary>
public class EditionTypeRepository(LibraryDbContext db) : IRepository<EditionType, int>
{
    /// <summary>
    /// Создаёт новый вид издания и сохраняет его в базе данных
    /// </summary>
    public async Task<EditionType> Create(EditionType entity)
    {
        db.EditionTypes.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Возвращает вид издания по идентификатору
    /// </summary>
    public async Task<EditionType?> Read(int entityId)
    {
        return await db.EditionTypes
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех видов изданий
    /// </summary>
    public async Task<IList<EditionType>> ReadAll()
    {
        return await db.EditionTypes
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет существующий вид издания
    /// </summary>
    public async Task<EditionType> Update(EditionType entity)
    {
        db.EditionTypes.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет вид издания по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.EditionTypes.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null)
            return false;

        db.EditionTypes.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
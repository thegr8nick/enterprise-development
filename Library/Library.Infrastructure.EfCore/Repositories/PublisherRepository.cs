using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий издательств Publisher
/// </summary>
public class PublisherRepository(LibraryDbContext db) : IRepository<Publisher, int>
{
    /// <summary>
    /// Создаёт новое издательство и сохраняет его в базе данных
    /// </summary>
    public async Task<Publisher> Create(Publisher entity)
    {
        db.Publishers.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Возвращает издательство по идентификатору
    /// </summary>
    public async Task<Publisher?> Read(int entityId)
    {
        return await db.Publishers
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех издательств
    /// </summary>
    public async Task<IList<Publisher>> ReadAll()
    {
        return await db.Publishers
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет существующее издательство
    /// </summary>
    public async Task<Publisher> Update(Publisher entity)
    {
        db.Publishers.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет издательство по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Publishers.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null)
            return false;

        db.Publishers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий читателей Reader
/// </summary>
public class ReaderRepository(LibraryDbContext db) : IRepository<Reader, int>
{
    /// <summary>
    /// Создаёт нового читателя и сохраняет его в базе данных
    /// </summary>
    public async Task<Reader> Create(Reader entity)
    {
        db.Readers.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Возвращает читателя по идентификатору вместе с историей выдач и книгами
    /// </summary>
    public async Task<Reader?> Read(int entityId)
    {
        return await db.Readers
            .Include(r => r.BookIssues)
                .ThenInclude(i => i.Book)
                    .ThenInclude(b => b!.Publisher)
            .Include(r => r.BookIssues)
                .ThenInclude(i => i.Book)
                    .ThenInclude(b => b!.EditionType)
            .FirstOrDefaultAsync(r => r.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех читателей
    /// </summary>
    public async Task<IList<Reader>> ReadAll()
    {
        return await db.Readers
            .AsNoTracking()
            .Include(r => r.BookIssues)
                .ThenInclude(i => i.Book)
                    .ThenInclude(b => b!.Publisher)
            .Include(r => r.BookIssues)
                .ThenInclude(i => i.Book)
                    .ThenInclude(b => b!.EditionType)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет данные читателя
    /// </summary>
    public async Task<Reader> Update(Reader entity)
    {
        db.Readers.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет читателя по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Readers.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null)
            return false;

        db.Readers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
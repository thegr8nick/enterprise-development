using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий фактов выдачи книг BookIssue
/// </summary>
public class BookIssueRepository(LibraryDbContext db) : IRepository<BookIssue, int>
{
    /// <summary>
    /// Создаёт новую выдачу книги и сохраняет её в базе данных
    /// </summary>
    public async Task<BookIssue> Create(BookIssue entity)
    {
        db.BookIssues.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Возвращает факт выдачи книги по идентификатору вместе с читателем и книгой
    /// </summary>
    public async Task<BookIssue?> Read(int entityId)
    {
        return await db.BookIssues
            .Include(x => x.Reader)
            .Include(x => x.Book)
                .ThenInclude(b => b!.Publisher)
            .Include(x => x.Book)
                .ThenInclude(b => b!.EditionType)
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех фактов выдачи книг вместе с читателями и книгами
    /// </summary>
    public async Task<IList<BookIssue>> ReadAll()
    {
        return await db.BookIssues
            .AsNoTracking()
            .Include(x => x.Reader)
            .Include(x => x.Book)
                .ThenInclude(b => b!.Publisher)
            .Include(x => x.Book)
                .ThenInclude(b => b!.EditionType)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет факт выдачи книги
    /// </summary>
    public async Task<BookIssue> Update(BookIssue entity)
    {
        db.BookIssues.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет факт выдачи книги по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.BookIssues.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null)
            return false;

        db.BookIssues.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
using Library.Domain;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий книг Book
/// </summary>
public class BookRepository(LibraryDbContext db) : IRepository<Book, int>
{
    /// <summary>
    /// Создаёт новую книгу и сохраняет её в базе данных
    /// </summary>
    public async Task<Book> Create(Book entity)
    {
        db.Books.Add(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Возвращает книгу по идентификатору вместе с издательством и видом издания
    /// </summary>
    public async Task<Book?> Read(int entityId)
    {
        return await db.Books
            .Include(x => x.Publisher)
            .Include(x => x.EditionType)
            .Include(x => x.Issues)
            .FirstOrDefaultAsync(x => x.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех книг вместе с издательством и видом издания
    /// </summary>
    public async Task<IList<Book>> ReadAll()
    {
        return await db.Books
            .AsNoTracking()
            .Include(x => x.Publisher)
            .Include(x => x.EditionType)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет существующую книгу
    /// </summary>
    public async Task<Book> Update(Book entity)
    {
        db.Books.Update(entity);
        await db.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Удаляет книгу по идентификатору
    /// </summary>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await db.Books.FirstOrDefaultAsync(x => x.Id == entityId);
        if (entity is null)
            return false;

        db.Books.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
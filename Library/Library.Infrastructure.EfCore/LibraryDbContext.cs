using Library.Domain.Data;
using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.EfCore;

/// <summary>
/// Контекст EF Core для доменной модели библиотеки;
/// Настроен под MS SQL Server: ограничения на строки, обязательность полей,
/// индексы, связи и начальное наполнение тестовыми данными
/// </summary>
public class LibraryDbContext(DbContextOptions<LibraryDbContext> options, DataSeeder seeder) : DbContext(options)
{
    /// <summary>
    /// Справочник издательств
    /// </summary>
    public DbSet<Publisher> Publishers => Set<Publisher>();

    /// <summary>
    /// Справочник видов издания
    /// </summary>
    public DbSet<EditionType> EditionTypes => Set<EditionType>();

    /// <summary>
    /// Каталог книг
    /// </summary>
    public DbSet<Book> Books => Set<Book>();

    /// <summary>
    /// Читатели библиотеки
    /// </summary>
    public DbSet<Reader> Readers => Set<Reader>();

    /// <summary>
    /// Факты выдачи книг
    /// </summary>
    public DbSet<BookIssue> BookIssues => Set<BookIssue>();

    /// <summary>
    /// Конфигурирует модель EF Core: таблицы, ключи, ограничения, связи, индексы и seed-данные
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("Publishers");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.HasIndex(x => x.Name);

            entity.HasData(seeder.Publishers);
        });

        modelBuilder.Entity<EditionType>(entity =>
        {
            entity.ToTable("EditionTypes");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            entity.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.HasIndex(x => x.Name);

            entity.HasData(seeder.EditionTypes);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Books");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            entity.Property(x => x.InventoryNumber)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(x => x.AlphabetCode)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            entity.Property(x => x.Authors)
                .HasMaxLength(400)
                .IsUnicode(true);

            entity.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(300)
                .IsUnicode(true);

            entity.Property(x => x.EditionTypeId)
                .IsRequired();

            entity.Property(x => x.PublisherId)
                .IsRequired();

            entity.Property(x => x.Year)
                .IsRequired();

            entity.HasIndex(x => x.InventoryNumber).IsUnique();
            entity.HasIndex(x => x.AlphabetCode).IsUnique();
            entity.HasIndex(x => x.Title);

            entity.HasOne(x => x.EditionType)
                .WithMany()
                .HasForeignKey(x => x.EditionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Publisher)
                .WithMany()
                .HasForeignKey(x => x.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(x => x.Issues)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(seeder.Books);
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.ToTable("Readers");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            entity.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(true);

            entity.Property(x => x.Address)
                .HasMaxLength(300)
                .IsUnicode(true);

            entity.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(x => x.RegistrationDate)
                .HasColumnType("datetime2");

            entity.HasIndex(x => x.FullName);
            entity.HasIndex(x => x.Phone);

            entity.HasMany(x => x.BookIssues)
                .WithOne(x => x.Reader)
                .HasForeignKey(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(seeder.Readers);
        });

        modelBuilder.Entity<BookIssue>(entity =>
        {
            entity.ToTable("BookIssues");

            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .ValueGeneratedNever()
                .IsRequired();

            entity.Property(x => x.BookId)
                .IsRequired();

            entity.Property(x => x.ReaderId)
                .IsRequired();

            entity.Property(x => x.IssueDate)
                .IsRequired()
                .HasColumnType("datetime2");

            entity.Property(x => x.Days)
                .IsRequired();

            entity.Property(x => x.ReturnDate)
                .HasColumnType("datetime2");

            entity.HasOne(x => x.Book)
                .WithMany(x => x.Issues)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Reader)
                .WithMany(x => x.BookIssues)
                .HasForeignKey(x => x.ReaderId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(seeder.BookIssues);
        });
    }
}
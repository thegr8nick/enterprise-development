using Library.Domain.Models;

namespace Library.Domain.Data;

/// <summary>
/// Класс, содержащий заранее подготовленные тестовые данные для доменной модели библиотеки
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Фиксированная точка времени в UTC для детерминированных данных
    /// </summary>
    public static readonly DateTime SeedNowUtc =
        DateTime.SpecifyKind(new DateTime(2025, 12, 16, 0, 0, 0), DateTimeKind.Utc);

    /// <summary>
    /// Список видов изданий
    /// </summary>
    public List<EditionType> EditionTypes { get; } =
    [
        new EditionType { Id = 1, Name = "Роман" },
        new EditionType { Id = 2, Name = "Повесть" },
        new EditionType { Id = 3, Name = "Учебник" },
        new EditionType { Id = 4, Name = "Справочник" },
        new EditionType { Id = 5, Name = "Фантастика" },
        new EditionType { Id = 6, Name = "Детектив" },
        new EditionType { Id = 7, Name = "Научная литература" },
        new EditionType { Id = 8, Name = "Сборник рассказов" },
        new EditionType { Id = 9, Name = "Историческая литература" },
        new EditionType { Id = 10, Name = "Документальная литература" },
    ];

    /// <summary>
    /// Список издательств
    /// </summary>
    public List<Publisher> Publishers { get; } =
    [
        new Publisher { Id = 1, Name = "АСТ" },
        new Publisher { Id = 2, Name = "Эксмо" },
        new Publisher { Id = 3, Name = "Просвещение" },
        new Publisher { Id = 4, Name = "Питер" },
        new Publisher { Id = 5, Name = "Наука" },
        new Publisher { Id = 6, Name = "Мир" },
        new Publisher { Id = 7, Name = "Олма" },
        new Publisher { Id = 8, Name = "Росмен" },
        new Publisher { Id = 9, Name = "Феникс" },
        new Publisher { Id = 10, Name = "Книжный Мир" },
    ];

    /// <summary>
    /// Список книг с заполненными ссылками на издательства и виды изданий
    /// </summary>
    public List<Book> Books { get; } =
    [
        new Book { Id = 1, InventoryNumber = "INV-001", AlphabetCode = "А-001", Authors = "А. Пушкин", Title = "Капитанская дочка", EditionTypeId = 2, PublisherId = 1, Year = 1836 },
        new Book { Id = 2, InventoryNumber = "INV-002", AlphabetCode = "Т-145", Authors = "Л. Толстой", Title = "Война и мир", EditionTypeId = 1, PublisherId = 2, Year = 1869 },
        new Book { Id = 3, InventoryNumber = "INV-003", AlphabetCode = "Д-019", Authors = "Ф. Достоевский", Title = "Идиот", EditionTypeId = 1, PublisherId = 1, Year = 1868 },
        new Book { Id = 4, InventoryNumber = "INV-004", AlphabetCode = "П-120", Authors = "И. Тургенев", Title = "Отцы и дети", EditionTypeId = 1, PublisherId = 2, Year = 1862 },
        new Book { Id = 5, InventoryNumber = "INV-005", AlphabetCode = "К-033", Authors = "Д. Лондон", Title = "Мартин Иден", EditionTypeId = 1, PublisherId = 7, Year = 1909 },
        new Book { Id = 6, InventoryNumber = "INV-006", AlphabetCode = "О-220", Authors = "А. Азимов", Title = "Основание", EditionTypeId = 5, PublisherId = 10, Year = 1951 },
        new Book { Id = 7, InventoryNumber = "INV-007", AlphabetCode = "С-001", Authors = "Р. Брэдбери", Title = "451 градус по Фаренгейту", EditionTypeId = 5, PublisherId = 8, Year = 1953 },
        new Book { Id = 8, InventoryNumber = "INV-008", AlphabetCode = "У-115", Authors = "А. Кристи", Title = "Убийство в Восточном экспрессе", EditionTypeId = 6, PublisherId = 9, Year = 1934 },
        new Book { Id = 9, InventoryNumber = "INV-009", AlphabetCode = "Ш-999", Authors = "А. Штраус", Title = "Основы физики", EditionTypeId = 7, PublisherId = 5, Year = 1999 },
        new Book { Id = 10, InventoryNumber = "INV-010", AlphabetCode = "Г-008", Authors = "Д. Карнеги", Title = "Как завоевывать друзей", EditionTypeId = 10, PublisherId = 4, Year = 1936 },
    ];

    /// <summary>
    /// Список читателей библиотеки, включающий персональные данные и дату регистрации
    /// </summary>
    public List<Reader> Readers { get; } =
    [
        new Reader { Id = 1, FullName = "Иванов Иван Иванович", Address = "ул. Ленина, 10", Phone = "89001001010", RegistrationDate = SeedNowUtc.AddYears(-2) },
        new Reader { Id = 2, FullName = "Петров Петр Петрович", Address = "ул. Кирова, 22", Phone = "89002002020", RegistrationDate = SeedNowUtc.AddYears(-1).AddDays(-10) },
        new Reader { Id = 3, FullName = "Сидорова Анна Павловна", Address = "ул. Гагарина, 3", Phone = "89003003030", RegistrationDate = SeedNowUtc.AddMonths(-9) },
        new Reader { Id = 4, FullName = "Кузнецов Михаил Олегович", Address = "ул. Победы, 55", Phone = "89004004040", RegistrationDate = SeedNowUtc.AddMonths(-8) },
        new Reader { Id = 5, FullName = "Смирнова Ольга Николаевна", Address = "ул. Горького, 77", Phone = "89005005050", RegistrationDate = SeedNowUtc.AddMonths(-6) },
        new Reader { Id = 6, FullName = "Васильев Дмитрий Андреевич", Address = "ул. Школьная, 8", Phone = "89006006060", RegistrationDate = SeedNowUtc.AddMonths(-5) },
        new Reader { Id = 7, FullName = "Попова Наталья Сергеевна", Address = "ул. Центральная, 1", Phone = "89007007070", RegistrationDate = SeedNowUtc.AddMonths(-4) },
        new Reader { Id = 8, FullName = "Федоров Алексей Ильич", Address = "ул. Советская, 45", Phone = "89008008080", RegistrationDate = SeedNowUtc.AddMonths(-3) },
        new Reader { Id = 9, FullName = "Алексеева Мария Петровна", Address = "ул. Молодежная, 12", Phone = "89009009090", RegistrationDate = SeedNowUtc.AddMonths(-2) },
        new Reader { Id = 10, FullName = "Соколова Ксения Дмитриевна", Address = "ул. Парковая, 5", Phone = "89001112233", RegistrationDate = SeedNowUtc.AddMonths(-1) },
    ];

    /// <summary>
    /// Список фактов выдачи книг
    /// </summary>
    public List<BookIssue> BookIssues { get; } =
    [
            new BookIssue { Id = 1,  BookId = 2, ReaderId = 1, IssueDate = SeedNowUtc.AddDays(-10), Days = 14, ReturnDate = null },
            new BookIssue { Id = 2,  BookId = 2, ReaderId = 1, IssueDate = SeedNowUtc.AddDays(-40), Days = 30, ReturnDate = SeedNowUtc.AddDays(-5) },
            new BookIssue { Id = 3,  BookId = 2, ReaderId = 1, IssueDate = SeedNowUtc.AddDays(-80), Days = 21, ReturnDate = SeedNowUtc.AddDays(-50) },
            new BookIssue { Id = 4,  BookId = 6, ReaderId = 1, IssueDate = SeedNowUtc.AddDays(-120), Days = 30, ReturnDate = SeedNowUtc.AddDays(-80) },
            new BookIssue { Id = 5,  BookId = 10, ReaderId = 1, IssueDate = SeedNowUtc.AddDays(-200), Days = 60, ReturnDate = SeedNowUtc.AddDays(-140) },
            new BookIssue { Id = 6,  BookId = 1, ReaderId = 1, IssueDate = SeedNowUtc.AddDays(-300), Days = 10, ReturnDate = SeedNowUtc.AddDays(-290) },
            new BookIssue { Id = 7,  BookId = 6, ReaderId = 2, IssueDate = SeedNowUtc.AddDays(-15), Days = 14, ReturnDate = null },
            new BookIssue { Id = 8,  BookId = 6, ReaderId = 2, IssueDate = SeedNowUtc.AddDays(-90), Days = 30, ReturnDate = SeedNowUtc.AddDays(-60) },
            new BookIssue { Id = 9,  BookId = 2, ReaderId = 2, IssueDate = SeedNowUtc.AddDays(-250), Days = 30, ReturnDate = SeedNowUtc.AddDays(-210) },
            new BookIssue { Id = 10, BookId = 5, ReaderId = 2, IssueDate = SeedNowUtc.AddDays(-60), Days = 14, ReturnDate = SeedNowUtc.AddDays(-40) },
            new BookIssue { Id = 11, BookId = 10, ReaderId = 2, IssueDate = SeedNowUtc.AddDays(-5), Days = 7, ReturnDate = null },
            new BookIssue { Id = 12, BookId = 1, ReaderId = 3, IssueDate = SeedNowUtc.AddDays(-20), Days = 7, ReturnDate = SeedNowUtc.AddDays(-5) },
            new BookIssue { Id = 13, BookId = 3, ReaderId = 3, IssueDate = SeedNowUtc.AddDays(-35), Days = 14, ReturnDate = SeedNowUtc.AddDays(-10) },
            new BookIssue { Id = 14, BookId = 6, ReaderId = 3, IssueDate = SeedNowUtc.AddDays(-400), Days = 30, ReturnDate = SeedNowUtc.AddDays(-350) },
            new BookIssue { Id = 15, BookId = 8, ReaderId = 4, IssueDate = SeedNowUtc.AddDays(-300), Days = 21, ReturnDate = SeedNowUtc.AddDays(-270) },
            new BookIssue { Id = 16, BookId = 5, ReaderId = 4, IssueDate = SeedNowUtc.AddDays(-33), Days = 20, ReturnDate = SeedNowUtc.AddDays(-5) },
            new BookIssue { Id = 17, BookId = 10, ReaderId = 4, IssueDate = SeedNowUtc.AddDays(-7), Days = 14, ReturnDate = null },
            new BookIssue { Id = 18, BookId = 1, ReaderId = 5, IssueDate = SeedNowUtc.AddDays(-2), Days = 10, ReturnDate = null },
            new BookIssue { Id = 19, BookId = 5, ReaderId = 5, IssueDate = SeedNowUtc.AddDays(-190), Days = 30, ReturnDate = SeedNowUtc.AddDays(-160) },
            new BookIssue { Id = 20, BookId = 6, ReaderId = 6, IssueDate = SeedNowUtc.AddDays(-220), Days = 30, ReturnDate = SeedNowUtc.AddDays(-190) },
            new BookIssue { Id = 21, BookId = 2, ReaderId = 6, IssueDate = SeedNowUtc.AddDays(-45), Days = 10, ReturnDate = SeedNowUtc.AddDays(-30) },
            new BookIssue { Id = 22, BookId = 7, ReaderId = 7, IssueDate = SeedNowUtc.AddDays(-18), Days = 7, ReturnDate = SeedNowUtc.AddDays(-8) },
            new BookIssue { Id = 23, BookId = 8, ReaderId = 8, IssueDate = SeedNowUtc.AddDays(-360), Days = 21, ReturnDate = SeedNowUtc.AddDays(-330) },
            new BookIssue { Id = 24, BookId = 4, ReaderId = 9, IssueDate = SeedNowUtc.AddDays(-800), Days = 14, ReturnDate = SeedNowUtc.AddDays(-780) },
            new BookIssue { Id = 25, BookId = 9, ReaderId = 10, IssueDate = SeedNowUtc.AddDays(-40), Days = 14, ReturnDate = SeedNowUtc.AddDays(-15) },
            new BookIssue { Id = 26, BookId = 10, ReaderId = 10, IssueDate = SeedNowUtc.AddDays(-120), Days = 30, ReturnDate = SeedNowUtc.AddDays(-90) },
            new BookIssue { Id = 27, BookId = 2, ReaderId = 10, IssueDate = SeedNowUtc.AddDays(-7), Days = 7, ReturnDate = null },
            new BookIssue { Id = 28, BookId = 1, ReaderId = 2, IssueDate = SeedNowUtc.AddDays(-75), Days = 10, ReturnDate = SeedNowUtc.AddDays(-65) },
            new BookIssue { Id = 29, BookId = 3, ReaderId = 5, IssueDate = SeedNowUtc.AddDays(-300), Days = 7, ReturnDate = SeedNowUtc.AddDays(-290) },
            new BookIssue { Id = 30, BookId = 6, ReaderId = 4, IssueDate = SeedNowUtc.AddDays(-8), Days = 14, ReturnDate = null },
    ];
}

using Bogus;
using Library.Application.Contracts.BookIssues;
using Library.DataGenerator.Options;
using Microsoft.Extensions.Options;

namespace Library.DataGenerator.Services;

/// <summary>
/// Сервис для генерации случайных BookIssue DTO
/// </summary>
public class BookIssueGenerator(IOptions<GeneratorOptions> options)
{
    private readonly Faker<BookIssueCreateUpdateDto> _faker = new Faker<BookIssueCreateUpdateDto>("ru")
        .RuleFor(x => x.BookId, f => f.Random.Int(1, options.Value.MaxBookId))
        .RuleFor(x => x.ReaderId, f => f.Random.Int(1, options.Value.MaxReaderId))
        .RuleFor(x => x.IssueDate, f => f.Date.Recent(30))
        .RuleFor(x => x.Days, f => f.Random.Int(options.Value.MinDays, options.Value.MaxDays))
        .RuleFor(x => x.ReturnDate, f => f.Random.Bool(0.3f) ? f.Date.Recent(7) : null);

    /// <summary>
    /// Генерирует заданное количество DTO для выдачи книги
    /// </summary>
    /// <param name="count">Количество генерируемых DTO</param>
    /// <returns>Последовательность DTO для выдачи книги</returns>
    public IEnumerable<BookIssueCreateUpdateDto> Generate(int count) => _faker.Generate(count);
}
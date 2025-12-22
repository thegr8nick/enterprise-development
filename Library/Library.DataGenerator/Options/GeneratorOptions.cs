namespace Library.DataGenerator.Options;

/// <summary>
/// Настройки генератора данных
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Название секции конфигурации
    /// </summary>
    public const string SectionName = "GeneratorOptions";

    /// <summary>
    /// Интервал между генерациями сообщений (в миллисекундах)
    /// </summary>
    public int IntervalMs { get; init; } = 2000;

    /// <summary>
    /// Максимальный Id книги для генерации (от 1 до MaxBookId)
    /// </summary>
    public int MaxBookId { get; init; } = 20;

    /// <summary>
    /// Максимальный Id читателя для генерации (от 1 до MaxReaderId)
    /// </summary>
    public int MaxReaderId { get; init; } = 20;

    /// <summary>
    /// Минимальное количество дней выдачи книги
    /// </summary>
    public int MinDays { get; init; } = 7;

    /// <summary>
    /// Максимальное количество дней выдачи книги
    /// </summary>
    public int MaxDays { get; init; } = 30;

    /// <summary>
    /// Количество сообщений за одну итерацию
    /// </summary>
    public int MessagesPerIteration { get; init; } = 1;

    /// <summary>
    /// Название очереди RabbitMQ
    /// </summary>
    public string QueueName { get; init; } = "book-issues-queue";
}
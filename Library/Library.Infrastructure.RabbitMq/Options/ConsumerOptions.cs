namespace Library.Infrastructure.RabbitMq.Options;

/// <summary>
/// Настройки Consumer для RabbitMQ
/// </summary>
public class ConsumerOptions
{
    /// <summary>
    /// Название секции конфигурации
    /// </summary>
    public const string SectionName = "ConsumerOptions";

    /// <summary>
    /// Название очереди RabbitMQ
    /// </summary>
    public string QueueName { get; init; } = "book-issues-queue";
}

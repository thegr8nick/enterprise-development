using System.Text;
using System.Text.Json;
using Library.Application.Contracts.BookIssues;
using Library.DataGenerator.Options;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Library.DataGenerator.Services;

/// <summary>
/// Сервис для публикации сообщений в RabbitMQ
/// </summary>
/// <param name="connection">Подключение к RabbitMQ</param>
/// <param name="options">Настройки генератора и очереди</param>
/// <param name="logger">Логгер</param>
public class RabbitMqPublisher(
    IConnection connection,
    IOptions<GeneratorOptions> options,
    ILogger<RabbitMqPublisher> logger) : IAsyncDisposable
{
    private readonly GeneratorOptions _options = options.Value;
    private readonly IChannel _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();
    private bool _queueDeclared;

    /// <summary>
    /// Публикует набор сообщений о выдачах книг в очередь RabbitMQ
    /// </summary>
    /// <param name="bookIssues">Коллекция DTO для создания или обновления выдачи книги</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task PublishAsync(IEnumerable<BookIssueCreateUpdateDto> bookIssues, CancellationToken cancellationToken = default)
    {
        await DeclareQueueIfNeeded(cancellationToken);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json"
        };

        var publishedCount = 0;

        foreach (var issue in bookIssues)
        {
            var json = JsonSerializer.Serialize(issue);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: _options.QueueName,
                mandatory: false,
                basicProperties: properties,
                body: body,
                cancellationToken: cancellationToken);

            publishedCount++;

            logger.LogInformation("Message published BookId={BookId} ReaderId={ReaderId}", issue.BookId, issue.ReaderId);
        }

        logger.LogInformation("Batch published Count={Count} Queue={QueueName}", publishedCount, _options.QueueName);
    }

    /// <summary>
    /// Освобождает ресурсы публикации RabbitMQ
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        logger.LogInformation("Disposing publisher resources Queue={QueueName}", _options.QueueName);

        try
        {
            await _channel.CloseAsync();
            logger.LogInformation("RabbitMQ channel closed Queue={QueueName}", _options.QueueName);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Error while closing RabbitMQ channel Queue={QueueName}", _options.QueueName);
        }
        finally
        {
            try
            {
                await _channel.DisposeAsync();
                logger.LogInformation("RabbitMQ channel disposed Queue={QueueName}", _options.QueueName);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Error while disposing RabbitMQ channel Queue={QueueName}", _options.QueueName);
            }

            GC.SuppressFinalize(this);
        }
    }

    private async Task DeclareQueueIfNeeded(CancellationToken cancellationToken)
    {
        if (_queueDeclared)
            return;

        await _channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);

        _queueDeclared = true;

        logger.LogInformation("Queue={QueueName} declared", _options.QueueName);
    }
}
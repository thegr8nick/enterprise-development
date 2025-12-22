using Library.DataGenerator.Options;
using Microsoft.Extensions.Options;

namespace Library.DataGenerator.Services;

/// <summary>
/// Фоновый сервис для периодической генерации сообщений о выдачах книг и их публикации в RabbitMQ
/// </summary>
/// <param name="generator">Генератор DTO для создания или обновления выдачи книги</param>
/// <param name="publisher">Публикатор сообщений RabbitMQ</param>
/// <param name="options">Настройки генерации</param>
/// <param name="logger">Логгер</param>
public class BookIssueGeneratorService(
    BookIssueGenerator generator,
    RabbitMqPublisher publisher,
    IOptions<GeneratorOptions> options,
    ILogger<BookIssueGeneratorService> logger) : BackgroundService
{
    private readonly GeneratorOptions _options = options.Value;

    /// <summary>
    /// Запускает цикл генерации и отправки сообщений до отмены токена
    /// </summary>
    /// <param name="stoppingToken">Токен отмены</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation(
            "BookIssue generator started IntervalMs={IntervalMs} MessagesPerIteration={MessagesPerIteration} MaxBookId={MaxBookId} MaxReaderId={MaxReaderId}",
            _options.IntervalMs, _options.MessagesPerIteration, _options.MaxBookId, _options.MaxReaderId);

        await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var batch = generator.Generate(_options.MessagesPerIteration);
                await publisher.PublishAsync(batch, stoppingToken);

                logger.LogDebug("Batch generated and published Count={Count}", _options.MessagesPerIteration);
            }
            catch (OperationCanceledException ex) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation(ex, "Cancellation requested, stopping generator loop");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to generate or publish messages");
            }

            try
            {
                await Task.Delay(_options.IntervalMs, stoppingToken);
            }
            catch (OperationCanceledException ex) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation(ex, "Cancellation requested during delay, stopping generator loop");
                break;
            }
        }

        logger.LogInformation("BookIssue generator stopped");
    }
}
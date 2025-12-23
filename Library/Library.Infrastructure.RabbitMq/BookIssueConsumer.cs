using Library.Application.Contracts;
using Library.Application.Contracts.BookIssues;
using Library.Infrastructure.RabbitMq.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Library.Infrastructure.RabbitMq;

/// <summary>
/// Фоновый сервис для получения и обработки BookIssue сообщений из RabbitMQ
/// </summary>
/// <param name="connection">Подключение к RabbitMQ</param>
/// <param name="scopeFactory">Фабрика для создания scope</param>
/// <param name="options">Настройки Consumer</param>
/// <param name="logger">Логгер</param>
public class BookIssueConsumer(
    IConnection connection,
    IServiceScopeFactory scopeFactory,
    IOptions<ConsumerOptions> options,
    ILogger<BookIssueConsumer> logger) : BackgroundService
{
    private readonly ConsumerOptions _options = options.Value;

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Consumer is running Queue: {QueueName}", _options.QueueName);

        await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: _options.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: stoppingToken);

        await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

        var consumerCancelled = new TaskCompletionSource<bool>();

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.UnregisteredAsync += (_, _) =>
        {
            logger.LogInformation("Consumer is stopping Queue: {QueueName}", _options.QueueName);
            consumerCancelled.TrySetResult(true);
            return Task.CompletedTask;
        };

        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var shouldAck = true;
            var shouldRequeue = false;

            try
            {
                var dto = JsonSerializer.Deserialize<BookIssueCreateUpdateDto>(message)
                    ?? throw new JsonException("Message deserialized to null");

                if (dto.BookId <= 0 || dto.ReaderId <= 0)
                    throw new ArgumentException("BookId and ReaderId must be more than 0");

                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider
                    .GetRequiredService<IApplicationService<BookIssueDto, BookIssueCreateUpdateDto, int>>();

                var result = await service.Create(dto);

                logger.LogInformation("BookIssue successfully created: Id={Id}, BookId={BookId}, ReaderId={ReaderId}",
                    result.Id, result.BookId, result.ReaderId);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning(ex, "Validation failed: {Message}", ex.Message);
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Message deserialization error: {Message}", ex.Message);
            }
            catch (ArgumentException ex)
            {
                logger.LogError(ex, "Invalid message data: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing the message: {Message}", ex.Message);
                shouldAck = false;
                shouldRequeue = true;
            }

            try
            {
                if (shouldAck)
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                else
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: shouldRequeue, cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to acknowledge message: {ExceptionType}", ex.GetType().Name);
            }
        };

        await channel.BasicConsumeAsync(
            queue: _options.QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await using var registration = stoppingToken.Register(() => consumerCancelled.TrySetCanceled());
        await consumerCancelled.Task;
    }
}
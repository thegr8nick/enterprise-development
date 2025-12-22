using Library.Api.Host.Options;
using Library.Application.Contracts;
using Library.Application.Contracts.BookIssues;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace Library.Api.Host.Consumers;

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

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

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

                logger.LogInformation("BookIssue successfully created: Id={Id}, BookId={BookId}, ReaderId={ReaderId}", result.Id, result.BookId, result.ReaderId);

                await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning("Validation failed: {Message}", ex.Message);

                try
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                }
                catch (AlreadyClosedException) { }
                catch (OperationCanceledException) { }
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Message deserialization error");

                try
                {
                    await channel.BasicAckAsync(ea.DeliveryTag, multiple: false, cancellationToken: stoppingToken);
                }
                catch (AlreadyClosedException) { }
                catch (OperationCanceledException) { }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing the message");

                try
                {
                    await channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true, cancellationToken: stoppingToken);
                }
                catch (AlreadyClosedException) { }
                catch (OperationCanceledException) { }
            }
        };

        await channel.BasicConsumeAsync(
            queue: _options.QueueName,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken);

        try
        {
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Consumer is stopping Queue: {QueueName}", _options.QueueName);
        }
    }
}
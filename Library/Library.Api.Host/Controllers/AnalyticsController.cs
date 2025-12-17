using Library.Application.Contracts;
using Library.Application.Contracts.Analytics;
using Library.Application.Contracts.Books;
using Library.Application.Contracts.Readers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для выполнения аналитических запросов по библиотеке
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает информацию о выданных книгах, упорядоченных по названию
    /// </summary>
    /// <returns>Список DTO для получения книг</returns>
    [HttpGet("issued-books")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookDto>>> GetIssuedBooksOrderedByTitle()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetIssuedBooksOrderedByTitle), GetType().Name);
        try
        {
            var res = await analyticsService.GetIssuedBooksOrderedByTitle();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetIssuedBooksOrderedByTitle), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetIssuedBooksOrderedByTitle), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает информацию о топ 5 читателей, прочитавших больше всего книг за заданный период
    /// </summary>
    /// <param name="periodStart">Начало периода в UTC</param>
    /// <param name="periodEnd">Конец периода в UTC</param>
    /// <returns>Список DTO для получения статистики по читателям</returns>
    [HttpGet("top-readers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ReaderIssuesStatDto>>> GetTop5ReadersByIssuesCount([FromQuery] DateTime periodStart, [FromQuery] DateTime periodEnd)
    {
        logger.LogInformation(
            "{method} method of {controller} is called with {start},{end} parameters",
            nameof(GetTop5ReadersByIssuesCount), GetType().Name, periodStart, periodEnd);

        if (periodEnd < periodStart)
            return BadRequest("periodEnd cannot be less than PeriodStart");

        try
        {
            var res = await analyticsService.GetTop5ReadersByIssuesCount(periodStart, periodEnd);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5ReadersByIssuesCount), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5ReadersByIssuesCount), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает информацию о читателях, бравших книги на наибольший период времени, упорядоченных по ФИО
    /// </summary>
    /// <returns>Список DTO для получения читателей</returns>
    [HttpGet("readers-max-loan-days")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ReaderDto>>> GetReadersByMaxLoanDaysOrderedByFullName()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetReadersByMaxLoanDaysOrderedByFullName), GetType().Name);
        try
        {
            var res = await analyticsService.GetReadersByMaxLoanDaysOrderedByFullName();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetReadersByMaxLoanDaysOrderedByFullName), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetReadersByMaxLoanDaysOrderedByFullName), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 наиболее популярных издательств за последний год
    /// </summary>
    /// <returns>Список DTO для получения статистики по издательствам</returns>
    [HttpGet("top-publishers-last-year")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<PublisherIssuesStatDto>>> GetTop5PublishersByIssuesCountLastYear()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5PublishersByIssuesCountLastYear), GetType().Name);
        try
        {
            var res = await analyticsService.GetTop5PublishersByIssuesCountLastYear(DateTime.UtcNow);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5PublishersByIssuesCountLastYear), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5PublishersByIssuesCountLastYear), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает топ 5 наименее популярных книг за последний год
    /// </summary>
    /// <returns>Список DTO для получения статистики по книгам</returns>
    [HttpGet("bottom-books-last-year")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookIssuesStatDto>>> GetBottom5BooksByIssuesCountLastYear()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetBottom5BooksByIssuesCountLastYear), GetType().Name);
        try
        {
            var res = await analyticsService.GetBottom5BooksByIssuesCountLastYear(DateTime.UtcNow);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetBottom5BooksByIssuesCountLastYear), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetBottom5BooksByIssuesCountLastYear), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
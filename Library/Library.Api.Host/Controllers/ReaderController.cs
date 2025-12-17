using Library.Application.Contracts.BookIssues;
using Library.Application.Contracts.Readers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с читателями
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ReaderController(
    IReaderService readerService,
    ILogger<ReaderController> logger)
    : CrudControllerBase<ReaderDto, ReaderCreateUpdateDto, int>(readerService, logger)
{
    /// <summary>
    /// Возвращает записи о выдачах книг читателю
    /// </summary>
    /// <param name="id">Идентификатор читателя</param>
    /// <returns>Список DTO для получения выдач книг</returns>
    [HttpGet("{id}/Issues")]
    [ProducesResponseType(typeof(IList<BookIssueDto>), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<BookIssueDto>>> GetIssues(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetIssues), GetType().Name, id);
        try
        {
            var res = await readerService.GetIssues(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetIssues), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetIssues), GetType().Name, ex);
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetIssues), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
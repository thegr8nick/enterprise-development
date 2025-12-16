using Library.Application.Contracts.Books;
using Library.Application.Contracts.BookIssues;
using Library.Application.Contracts.EditionTypes;
using Library.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с книгами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookController(
    IBookService bookService,
    ILogger<BookController> logger)
    : CrudControllerBase<BookDto, BookCreateUpdateDto, int>(bookService, logger)
{
    /// <summary>
    /// Возвращает записи о выдачах книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
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
            var res = await bookService.GetIssues(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetIssues), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetIssues), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает вид издания книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>DTO для получения вида издания</returns>
    [HttpGet("{id}/EditionType")]
    [ProducesResponseType(typeof(EditionTypeDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<EditionTypeDto>> GetEditionType(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetEditionType), GetType().Name, id);
        try
        {
            var res = await bookService.GetEditionType(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetEditionType), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetEditionType), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Возвращает издательство книги
    /// </summary>
    /// <param name="id">Идентификатор книги</param>
    /// <returns>DTO для получения издательства</returns>
    [HttpGet("{id}/Publisher")]
    [ProducesResponseType(typeof(PublisherDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetPublisher), GetType().Name, id);
        try
        {
            var res = await bookService.GetPublisher(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPublisher), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPublisher), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
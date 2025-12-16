using Library.Application.Contracts;
using Library.Application.Contracts.BookIssues;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с выдачами книг
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookIssueController(
    IApplicationService<BookIssueDto, BookIssueCreateUpdateDto, int> appService,
    ILogger<BookIssueController> logger)
    : CrudControllerBase<BookIssueDto, BookIssueCreateUpdateDto, int>(appService, logger);
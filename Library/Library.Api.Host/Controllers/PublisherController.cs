using Library.Application.Contracts;
using Library.Application.Contracts.Publishers;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с издательствами
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PublisherController(
    IApplicationService<PublisherDto, PublisherCreateUpdateDto, int> appService,
    ILogger<PublisherController> logger)
    : CrudControllerBase<PublisherDto, PublisherCreateUpdateDto, int>(appService, logger);
using Library.Application.Contracts;
using Library.Application.Contracts.EditionTypes;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Host.Controllers;

/// <summary>
/// Контроллер для работы с видами изданий
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class EditionTypeController(
    IApplicationService<EditionTypeDto, EditionTypeCreateUpdateDto, int> appService,
    ILogger<EditionTypeController> logger)
    : CrudControllerBase<EditionTypeDto, EditionTypeCreateUpdateDto, int>(appService, logger);
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevCompany.Application.Services.Costumer;
using RevCompany.Contracts.Costumer;
using RevCompany.Domain.Entities.Costumer;

namespace RevCompany.API.Controllers;

[ApiController]
[Route("v1/costumer")]
[Authorize]
public class CostumerController : ControllerBase
{
  private readonly ICostumerService _costumerService;
  private readonly ILogger<CostumerController> _logger;

  public CostumerController(
    ICostumerService _costumerService,
    ILogger<CostumerController> logger)
  {
    this._costumerService = _costumerService;
    _logger = logger;
  }

  [HttpGet("listAll")]
  public IActionResult GetAll()
  {
    var result = this._costumerService.GetAll();

    ListQueryResponse response = new(
      [.. result
        .Select(qr =>
          new QueryResponse(
            qr.costumer.Id,
            qr.costumer.Name,
            qr.costumer.Email.value,
            qr.costumer.Phone,
            qr.costumer.GetStatus()
          )
        )]
    );
    
    return Ok(response);   
  }
  
  [HttpPost("create")]
  public IActionResult CreateCostumer(CostumerRequestVo request)
  {
    var result = this._costumerService.Create(
      request.Name,
      request.Email,
      request.Phone,
      request.Address
    );

    var response = new CostumerResponseVo(
      result.costumer.Id,
      result.costumer.Name,
      result.costumer.GetStatus()
    );
    
    return Ok(response);   
  }

  [HttpPut("update/{id}")]
  public IActionResult UpdateCostumer(string id, CostumerRequestVo request)
  {
    var result = this._costumerService.Update(
      id,
      request.Name,
      request.Email,
      request.Phone,
      request.Address
    );

    var response = new CostumerResponseVo(
      result.costumer.Id,
      result.costumer.Name,
      result.costumer.GetStatus()
    );
    
    return Ok(response);   
  }
}

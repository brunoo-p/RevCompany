using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevCompany.Application.Services.Costumer;
using RevCompany.Contracts.Costumer;

namespace RevCompany.API.Controllers;

[ApiController]
[Route("v1/costumer")]
// [Authorize]
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
  public async Task<IActionResult> GetAllAsync()
  {
    var result = await this._costumerService.GetAllAsync();

    ListQueryResponse response = new(
      [.. result
        .Select(qr =>
          new QueryResponse(
            qr.costumer.Id,
            qr.costumer.Name,
            qr.costumer.Email,
            qr.costumer.Phone,
            qr.costumer.Status
          )
        )]
    );
    
    return Ok(response);   
  }
  
  [HttpPost("create")]
  public async Task<IActionResult> CreateCostumer(CostumerRequestVo request)
  {
    var result = await this._costumerService.CreateAsync(
      request.Name,
      request.Email,
      request.Phone,
      request.Address
    );

    var response = new CostumerResponseVo(
      result.costumer.Id,
      result.costumer.Name,
      result.costumer.Status
    );
    
    return Ok(response);   
  }

  [HttpPut("{id}/update")]
  public async Task<IActionResult> UpdateCostumer(string id, [FromBody] CostumerRequestVo request)
  {
    var result = await this._costumerService.UpdateAsync(
      id,
      request.Name,
      request.Email,
      request.Phone,
      request.Address,
      request.Status
    );

    var response = new CostumerResponseVo(
      result.costumer.Id,
      result.costumer.Name,
      result.costumer.Status
    );
    
    return Ok(response);   
  }
}

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

  public CostumerController(
    ICostumerService _costumerService
    )
  {
    this._costumerService = _costumerService;
  }

  [HttpPost("create")]
  public async Task<ActionResult<CostumerResponseVo>> CreateCostumer(CostumerRequestVo request)
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

  [HttpGet("list")]
  public async Task<ActionResult<ListQueryResponse>> GetAllAsync()
  {
    var result = await this._costumerService.GetAllAsync(null);

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

  [HttpGet("{id}")]
  public async Task<ActionResult<QueryResponse>> GetById([FromRoute] Guid id)
  {
    var result = await this._costumerService.GetByIdAsync(id);
    var response = new QueryResponse(
      result.costumer.Id,
      result.costumer.Name,
      result.costumer.Email,
      result.costumer.Phone,
      result.costumer.Status
    );
    return Ok(response);   
  }

  [HttpPut("{id}")]
  public async Task<ActionResult<CostumerResponseVo>> UpdateCostumer([FromRoute] Guid id, [FromBody] CostumerRequestVo request)
  {
    var result = await this._costumerService.UpdateAsync(
      id,
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

  [HttpDelete("{id}")]
  public IActionResult DeleteCostumer([FromRoute] string id) {

    this._costumerService.Delete(id);
    return Ok();
  }
}

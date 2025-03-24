using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevCompany.Application.Services.Costumer;
using RevCompany.Contracts.Order;


namespace RevCompany.API.Controllers;

[ApiController]
[Route("v1/order")]
[Authorize]
public class OrderController : ControllerBase
{
  private readonly IOrderService _orderService;
  private readonly ILogger<OrderController> _logger;

  public OrderController(
    IOrderService orderService,
    ILogger<OrderController> logger)
  {
    this._orderService = orderService;
    _logger = logger;
  }
  
  [HttpPost("create")]
  public IActionResult CreateOrder(OrderRequestVo request)
  {
    var result = this._orderService.Create(
      request.CostumerId,
      request.Items
    );

    var response = new OrderResponseVo(
      result.order.Id,
      result.order.CostumerId,
      result.order.Amount,
      result.order.GetStatus()
    );
    
    return Ok(response);   
  }

  [HttpGet("list/{costumerId}")]
  public IActionResult GetByCostumerId(string costumerId)
  {
    var orders = this._orderService.GetByCostumerId(costumerId);

    ListQueryResponse response = new(
      [.. orders
        .Select(qr =>
          new QueryResponse(
            qr.order.Id,
            qr.order.CostumerId,
            qr.order.Items,
            qr.order.Amount,
            qr.order.GetStatus()
          )
        )]
    );
    
    return Ok(response);   
  }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevCompany.Application.Services.Costumer;
using RevCompany.Contracts.Order;


namespace RevCompany.API.Controllers;

[ApiController]
[Route("v1/order/status")]
[Authorize]
public class OrderStatusController : ControllerBase
{
  private readonly IOrderService _orderService;
  private readonly ILogger<OrderController> _logger;

  public OrderStatusController(
    IOrderService orderService,
    ILogger<OrderController> logger)
  {
    this._orderService = orderService;
    _logger = logger;
  }
  
  [HttpPut("{id}")]
  public IActionResult UpdateOrderStatus(string id, string newStatus)
  {
    var result = this._orderService.Update(
      id,
      newStatus
    );

    var response = new OrderResponseVo(
      result.order.Id,
      result.order.CostumerId,
      result.order.Amount,
      result.order.GetStatus()
    );
    
    return Ok(response);   
  }
}

using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 
[Route("[controller]")]
public class OrderItemController:ControllerBase
{
    private OrderItemServices _orderItemServices;

    public OrderItemController(OrderItemServices orderItemServices)
    {
        _orderItemServices = orderItemServices;
    }
    
    [HttpGet("GetOrderItem")]
    public async Task<Response<List<OrderItemDto>>> GetOrderItem()
    {
        return await _orderItemServices.GetOrderItem();
    }

    [HttpGet("GetOrderItemBId(")]
    public async Task<Response<OrderItem>> GetOrderItemBId(int id)
    {
        return await _orderItemServices.GetOrderItemBId(id);
    }

    [HttpPost("AddOrderItem")]
    public async Task<Response<OrderItemDto>> AddOrderItem(OrderItemDto orderItemDto)
    {
        if (ModelState.IsValid)
        {
        return await _orderItemServices.AddOrderItem(orderItemDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<OrderItemDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("UpdateOrderItem")]
    public async Task<Response<OrderItemDto>> UpdateOrderItem(OrderItemDto orderItemDto)
    {
        if (ModelState.IsValid)
        {
           return await _orderItemServices.UpdateOrderItem(orderItemDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<OrderItemDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteOrderItem")]
    public async Task DeleteOrderItem(int id)
    {
        await _orderItemServices.DeleteOrderItem(id);
    }
}
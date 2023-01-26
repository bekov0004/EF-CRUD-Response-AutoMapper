using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 
[Route("[controller]")]
public class OrderController:ControllerBase
{
    private OrderServices _orderServices;

    public OrderController(OrderServices orderServices)
    {
        _orderServices = orderServices;
    }
    
    [HttpGet("GetOrder")]
    public async Task<Response<List<OrderDto>>> GetOrder()
    {
        return await _orderServices.GetOrder();
    }

    [HttpGet("GetOrderBId")]
    public async Task<Response<Order>> GetOrderBId(int id)
    {
        return await _orderServices.GetOrderBId(id);
    }

    [HttpPost("AddOrder")]
    public async Task<Response<OrderDto>> AddOrder(OrderDto orderDto)
    {
        if (ModelState.IsValid)
        {
        return await _orderServices.AddOrder(orderDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<OrderDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("UpdateOrder")]
    public async Task<Response<OrderDto>> UpdateOrder(OrderDto orderDto)
    {
        if (ModelState.IsValid)
        {
        return await _orderServices.UpdateOrder(orderDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<OrderDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteOrder")]
    public async Task DeleteOrder(int id)
    {
        await _orderServices.DeleteOrder(id);
    }
}
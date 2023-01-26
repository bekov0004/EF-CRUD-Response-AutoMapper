using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderItemServices
{
    private readonly DataContext _context; 
    private readonly IMapper _mapper; 
   
    public OrderItemServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderItemDto>>> GetOrderItem()
    {
        var result = await _context.OrderItems.ToListAsync();
        return new Response<List<OrderItemDto>>(_mapper.Map<List<OrderItemDto>>(result));
    }

    public async Task<Response<OrderItem>> GetOrderItemBId(int id)
    {
        return new Response<OrderItem>( await _context.OrderItems.FirstOrDefaultAsync(x => x.OrderId == id));
    }

    public async Task<Response<OrderItemDto>> AddOrderItem(OrderItemDto orderItemDto)
    {
        try
        {
            var mapped = _mapper.Map<OrderItem>(orderItemDto);
            await _context.OrderItems.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<OrderItemDto>(orderItemDto);
        }
        catch (Exception ex)
        {
            return new Response<OrderItemDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<OrderItemDto>> UpdateOrderItem(OrderItemDto orderItemDto)
    {
        try
        {
            var mapped = _mapper.Map<OrderItem>(orderItemDto); 
            _context.OrderItems.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<OrderItemDto>(orderItemDto);
        }
        catch (Exception ex)
        {
            return new Response<OrderItemDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });

        }
        
    }

    public async Task<Response<string>> DeleteOrderItem(int id)
    {
        var existing=await _context.Addresses.FirstAsync(x => x.Id == id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });
        var delete =await _context.OrderItems.FirstAsync(x => x.OrderId == id);
        _context.OrderItems.Remove(delete);
         await _context.SaveChangesAsync();
         return new Response<string>("Delete Successfully");

    }

}
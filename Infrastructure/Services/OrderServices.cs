using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public OrderServices(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<OrderDto>>> GetOrder()
    {
        var result = await _context.Orders.ToListAsync();
        return new Response<List<OrderDto>>(_mapper.Map<List<OrderDto>>(result));
    }

    public async Task<Response<Order>> GetOrderBId(int id)
    {
        return new Response<Order>(await _context.Orders.FirstOrDefaultAsync(x => x.Id == id)) ;
    }

    public async Task<Response<OrderDto>> AddOrder(OrderDto orderDto)
    {
        try
        {
            var mapped = _mapper.Map<Order>(orderDto);
            await _context.Orders.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<OrderDto>(orderDto);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });

        }
        
    }

    public async Task<Response<OrderDto>> UpdateOrder(OrderDto orderDto)
    {
        try
        {
            var mapped = _mapper.Map<Order>(orderDto);
             _context.Orders.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<OrderDto>(orderDto);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<string>>  DeleteOrder(int id)
    {
        var existing=await _context.Addresses.FirstAsync(x => x.Id == id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });
        var delete =await _context.Orders.FirstAsync(x => x.Id == id);
        _context.Orders.Remove(delete);
      await _context.SaveChangesAsync(); 
      return new Response<string>("Delete Successfully");

    }

}
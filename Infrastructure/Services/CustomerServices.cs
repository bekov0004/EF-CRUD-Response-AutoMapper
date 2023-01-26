using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class CustomerServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public CustomerServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Response<List<CustomerDto>>> GetCustomer()
    {
        var result = await _context.Customers.ToListAsync();
        return new Response<List<CustomerDto>>(_mapper.Map<List<CustomerDto>>(result)) ;
    }

    public async Task<Response<Customer>> GetCustomerById(int id)
    {
        return new Response<Customer>(await _context.Customers.FirstOrDefaultAsync(x => x.Id == id)) ;
    }

    public async Task<Response<CustomerDto>> AddCustomer(CustomerDto customerDto)
    {
        try
        {
            var mapped = _mapper.Map<Customer>(customerDto);
            await _context.Customers.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<CustomerDto>(customerDto);
        }
        catch (Exception ex)
        {
            return new Response<CustomerDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<CustomerDto>> UpdateCustomer(CustomerDto customerDto)
    {
        try
        {
            var mapped = _mapper.Map<Customer>(customerDto);
            _context.Customers.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<CustomerDto>(customerDto);
        }
        catch (Exception ex)
        {
            return new Response<CustomerDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<string>> DeleteCustomer(int id)
    {
        var existing=await _context.Addresses.FirstAsync(x => x.Id == id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });
        var delete = await _context.Customers.FirstAsync(e => e.Id == id);
        _context.Customers.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Delete Successfully");

    }
    
}

using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AddressServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AddressServices(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<AddressDto>>> GetAddress()
    {
        var result = await _context.Addresses.ToListAsync();
        return new Response<List<AddressDto>>(_mapper.Map<List<AddressDto>>(result));
    }

    public async Task<Response<Address>> GetAddressById(int id)
    {
        return new Response<Address>(await _context.Addresses.FirstOrDefaultAsync(x => x.Id == id)) ;
    }

    public async Task<Response<AddressDto>> AddAdress(AddressDto addressDto)
    {
        try
        {
            var mapped = _mapper.Map<Address>(addressDto);
            await _context.Addresses.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddressDto>(addressDto);
        }
        catch (Exception ex)
        {
            return new Response<AddressDto>(HttpStatusCode.InternalServerError, new List<string>() {ex.Message});
        }
        
    }

    public async Task<Response<AddressDto>> UpdateAddress(AddressDto addressDto)
    {
        try
        {
            var mapped = _mapper.Map<Address>(addressDto);
            _context.Addresses.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<AddressDto>(addressDto);
        }
        catch (Exception ex)
        {
            return new Response<AddressDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<string>> DeleteAddress(int id)
    {
        var existing=await _context.Addresses.FirstAsync(x => x.Id == id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });
        var delete = await _context.Addresses.FirstAsync(x => x.Id == id);
        _context.Addresses.Remove(delete);
        await _context.SaveChangesAsync();
        return new Response<string>("Delete Successfully");
    }
}
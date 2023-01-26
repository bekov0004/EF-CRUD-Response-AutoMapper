using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class SupplierServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public SupplierServices(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<SupplierDto>>> GetSupplier()
    {
        var result = await _context.Suppliers.ToListAsync();
        return new Response<List<SupplierDto>>(_mapper.Map<List<SupplierDto>>(result));
    }

    public async Task<Response<Supplier>> GetSupplierById(int id)
    {
        return new Response<Supplier>(await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id));
    }

    public async Task<Response<SupplierDto>> AddSupplier(SupplierDto supplierDto)
    {
        try
        {
            var mapped = _mapper.Map<Supplier>(supplierDto);
            await _context.Suppliers.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<SupplierDto>(supplierDto);
        }
        catch (Exception ex)
        {
             return new Response<SupplierDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<SupplierDto>> UpdateSupplier(SupplierDto supplierDto)
    {
        try
        {
            var mapped = _mapper.Map<Supplier>(supplierDto);
            _context.Suppliers.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<SupplierDto>(supplierDto);
        }
        catch (Exception ex)
        {
            return new Response<SupplierDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });

        }
        
    }

    public async Task<Response<string>> DeleteSupplier(int id)
    {
        var existing=await _context.Addresses.FirstAsync(x => x.Id == id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });
        var delete =await _context.Suppliers.FirstAsync(x => x.Id == id);
        _context.Suppliers.Remove(delete);
      await _context.SaveChangesAsync();
      return new Response<string>("Delete Successfully");
    }

}
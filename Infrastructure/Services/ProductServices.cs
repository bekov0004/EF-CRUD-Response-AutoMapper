using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProductServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ProductServices(DataContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<ProductDto>>> GetProduct()
    {
        var result = await _context.Products.ToListAsync();
        return new Response<List<ProductDto>>(_mapper.Map<List<ProductDto>>(result)) ;
    }

    public async Task<Response<Product>> GetProductBId(int id)
    {
        return new Response<Product>(await _context.Products.FirstOrDefaultAsync(x => x.Id == id)) ;
    }

    public async Task<Response<ProductDto>> AddProduct(ProductDto productDto)
    {
        try
        {
            var mapped = _mapper.Map<Product>(productDto);
            await _context.Products.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<ProductDto>(productDto);
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });

        }
        
    }

    public async Task<Response<ProductDto>> UpdateProduct(ProductDto productDto)
    {
        try
        {
            var mapped = _mapper.Map<Product>(productDto);
             _context.Products.Update(mapped);
            await _context.SaveChangesAsync();
            return new Response<ProductDto>(productDto);
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>(HttpStatusCode.InternalServerError, new List<string>() { ex.Message });
        }
        
    }

    public async Task<Response<string>> DeleteProduct(int id)
    {
        var existing=await _context.Addresses.FirstAsync(x => x.Id == id);
        if (existing == null) return new Response<string>(HttpStatusCode.NotFound, new List<string>() { "Not Found" });
        var delete =await _context.Products.FirstAsync(x => x.Id == id);
        _context.Products.Remove(delete);
      await _context.SaveChangesAsync();
      return new Response<string>("Delete Successfully");
    }

}
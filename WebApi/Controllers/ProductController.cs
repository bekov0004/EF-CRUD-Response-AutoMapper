using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 
[Route("[controller]")]
public class ProductController:ControllerBase
{
    private ProductServices _productServices;

    public ProductController(ProductServices productServices)
    {
        _productServices = productServices;
    }
    
    [HttpGet("GetProduct")]
    public async Task<Response<List<ProductDto>>> GetProduct()
    {
        return await _productServices.GetProduct();
    }

    [HttpGet("GetProductBId")]
    public async Task<Response<Product>> GetProductBId(int id)
    {
        return await _productServices.GetProductBId(id);
    }

    [HttpPost("AddProduct")]
    public async Task<Response<ProductDto>> AddProduct(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
       return await _productServices.AddProduct(productDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<ProductDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("UpdateProduct")]
    public async Task<Response<ProductDto>> UpdateProduct(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
      return await _productServices.UpdateProduct(productDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<ProductDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteProduct")]
    public async Task DeleteProduct(int id)
    {
        await _productServices.DeleteProduct(id);
    }
}
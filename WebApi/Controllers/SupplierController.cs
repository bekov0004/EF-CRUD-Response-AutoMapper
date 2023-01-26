using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[ApiController]
[Route("[controller]")]
public class SupplierController:ControllerBase
{
    private SupplierServices _supplierServices;

    public SupplierController(SupplierServices supplierServices)
    {
        _supplierServices = supplierServices;
    }
    
    [HttpGet("GetSupplier")]
    public async Task<Response<List<SupplierDto>>> GetSupplier()
    {
        return await _supplierServices.GetSupplier();
    }

    [HttpGet("GetSupplierById")]
    public async Task<Response<Supplier>> GetSupplierById(int id)
    {
        return await _supplierServices.GetSupplierById(id);
    }

    [HttpPost("AddSupplier")]
    public async Task<Response<SupplierDto>> AddSupplier(SupplierDto supplierDto)
    {
        if (ModelState.IsValid)
        {
        return await _supplierServices.AddSupplier(supplierDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<SupplierDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("UpdateSupplier")]
    public async Task<Response<SupplierDto>> UpdateSupplier(SupplierDto supplierDto)
    {
        if (ModelState.IsValid)
        {
       return await _supplierServices.UpdateSupplier(supplierDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<SupplierDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteSupplier")]
    public async Task DeleteSupplier(int id)
    {
        await _supplierServices.DeleteSupplier(id);
    }
}
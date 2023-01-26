using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers; 
[Route("[controller]")]
public class CustomerController:ControllerBase
{
    private CustomerServices _customerServices;

    public CustomerController(CustomerServices customerServices)
    {
        _customerServices = customerServices;
    }

    [HttpGet("GerCustomer")]
    public async Task<Response<List<CustomerDto>>> GetCutomer()
    {
        return await _customerServices.GetCustomer();
    }

    [HttpGet("GetCustomerById")]
    public async Task<Response<Customer>> GetCustomerById(int id)
    {
       return await _customerServices.GetCustomerById(id);
    }

    [HttpPost("AddCustomer")]
    public async Task<Response<CustomerDto>> AddCustomer(CustomerDto customerDto)
    {
        if (ModelState.IsValid)
        {
      return await _customerServices.AddCustomer(customerDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CustomerDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("UpdateCustomer")]
    public async Task<Response<CustomerDto>> UpdateCustomer(CustomerDto customerDto)
    {
        if (ModelState.IsValid)
        {
        return await _customerServices.UpdateCustomer(customerDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<CustomerDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteCustomer")]
    public async Task DeleteCustomer(int id)
    {
        await _customerServices.DeleteCustomer(id);
    }
    
}
using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Domain.Wrapper;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("[controller]")]
public class AddController:ControllerBase
{
    private AddressServices _addressServices;

    public AddController(AddressServices addressServices)
    {
        _addressServices = addressServices;
    }

    [HttpGet("GetAddress")]
    public async Task<Response<List<AddressDto>>> GetAddress()
    {
        return await _addressServices.GetAddress();
    }

    [HttpGet("GetAddressById")]
    public async Task<Response<Address>> GetAddressById(int id)
    {
        return await _addressServices.GetAddressById(id);
    }

    [HttpPost("AddAddress")]
    public async Task<Response<AddressDto>> AddAddress(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
       return await _addressServices.AddAdress(addressDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddressDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpPut("UpdateAddress")]
    public async Task<Response<AddressDto>> UpdateAddress(AddressDto addressDto)
    {
        if (ModelState.IsValid)
        {
          return await _addressServices.UpdateAddress(addressDto);
        }
        else
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).ToList();
            return new Response<AddressDto>(HttpStatusCode.BadRequest, errors);
        }
    }

    [HttpDelete("DeleteAddress")]
    public async Task DeleteAddress(int id)
    {
        await _addressServices.DeleteAddress(id);
    }
}
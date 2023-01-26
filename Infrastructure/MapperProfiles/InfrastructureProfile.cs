using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastructure.MapperProfiles;

public class InfrastructureProfile:Profile
{
    public InfrastructureProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>();
        
        CreateMap<Order, OrderDto>();
        CreateMap<OrderDto, Order>();
        
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItemDto, OrderItem>();
        
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        
        CreateMap<Address, AddressDto>();
        CreateMap<AddressDto, Address>();
        
        CreateMap<Supplier, SupplierDto>();
        CreateMap<SupplierDto, Supplier>();
        
    }
}
namespace Domain.Dtos;

public class AddressDto
{
    public int Id { get; set; } 
    public string Address1 { get; set; } 
    public string Address2 { get; set; } 
    public string CityName { get; set; } 
    public string PostalCode { get; set; }
    public int CustomerId { get; set; }
}
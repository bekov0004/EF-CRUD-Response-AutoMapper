namespace Domain.Dtos;

public class ProductDto
{
    public int Id { get; set; } 
    public string ProductName { get; set; } 
    public int SupplierId { get; set; } 
    public DateTime OrderDate { get; set; } 
    public int TotalAmount { get; set; } 
}
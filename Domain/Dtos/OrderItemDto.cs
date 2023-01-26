namespace Domain.Dtos;

public class OrderItemDto
{ 
    public int OrderId { get; set; } 
    public int ProductId { get; set; } 
    public decimal UnitPrice { get; set; } 
    public int Quantity { get; set; } 
}
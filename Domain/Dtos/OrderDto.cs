namespace Domain.Dtos;

public class OrderDto
{
    public int Id { get; set; } 
    public string OrderNumbrer { get; set; } 
    public DateTime OrderDate { get; set; } 
    public decimal TotalAmount { get; set; } 
    public int CustomerId { get; set; } 
}